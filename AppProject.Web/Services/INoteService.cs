using AppProject.Web.Data;
using AppProject.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppProject.Web.Services
{
    public interface INoteService
    {
        Task<List<Note>> GetAllNotesAsync();
        Task<List<Note>> GetNotesByCategoryAsync(int categoryId);
        Task<List<Note>> SearchNotesAsync(string query);
        Task<Note> GetNoteByIdAsync(int id);
        Task AddNoteAsync(Note note);
        Task UpdateNoteAsync(Note note);
        Task DeleteNoteAsync(int id);
    }

    public class NoteService : INoteService
    {
        private readonly DataContext _context;

        public NoteService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Note>> GetAllNotesAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<List<Note>> GetNotesByCategoryAsync(int categoryId)
        {
            return await _context.Notes.Where(n => n.CategoryId == categoryId).ToListAsync();
        }

        public async Task<List<Note>> SearchNotesAsync(string query)
        {
            return await _context.Notes.Where(n => n.Title.Contains(query) || n.Content.Contains(query)).ToListAsync();
        }

        public async Task<Note> GetNoteByIdAsync(int id)
        {
            return await _context.Notes.FindAsync(id);
        }

        public async Task AddNoteAsync(Note note)
        {
            _context.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNoteAsync(Note note)
        {
            _context.Update(note);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
        }
    }
}
