﻿using VSHCTwebApp.Components.Models;
using System.Collections.Generic;

namespace VSHCTwebApp.Components.Services
{
    public class NoteService
    {
        private readonly List<Note> _notes = new List<Note>();

        public void AddNote(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }

            note.CreatedAt = DateTime.Now;
            _notes.Add(note);
            Console.WriteLine($"Записей в сервисе: {_notes.Count}");
        }

        public List<Note> GetNotes() => _notes;

        public Note GetNoteByTitle(string title)
        {
            return _notes.FirstOrDefault(n => n.Title == title);
        }
    }
}
