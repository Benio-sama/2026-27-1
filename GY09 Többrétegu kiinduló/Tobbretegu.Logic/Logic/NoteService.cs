using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Tobbretegu.Model;

namespace Tobbretegu.Logic
{
    public class NoteService
    {
        private NoteList allNotes;
        public NoteService() 
        { 
            allNotes = Repository.NoteRepo.LoadNotesFromFile();
        }
        public NoteList FilterNotes(Guid userId)
        {
            var list = new NoteList();
            foreach (var item in allNotes)
            {
                if (item.Assignee == userId)
                    list.Add(item);
            }
            return list;
        }
        public void Add(Note newNote)
        {
            allNotes.Add(newNote);
            Repository.NoteRepo.SaveNotesToFile(allNotes);
        }
        public void Remove(Note note)
        {
            allNotes.Remove(note);
            Repository.NoteRepo.SaveNotesToFile(allNotes);
        }

        public void SaveNotesToFile()
        {
            Repository.NoteRepo.SaveNotesToFile(allNotes);
        }
    }
}
