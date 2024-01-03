﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MoncayoLoaizaJ_TareaMVVM.Models;


namespace MoncayoLoaizaJ_TareaMVVM.ViewModelsJM
{
    internal class NotesViewModelJM : IQueryAttributable
    {
        public ObservableCollection<ViewModelsJM.NoteViewModelJM> AllNotes { get; }
        public ICommand NewCommand { get; }
        public ICommand SelectNoteCommand { get; }

        public NotesViewModelJM()
        {
            AllNotes = new ObservableCollection<ViewModelsJM.NoteViewModelJM>(Models.Note.LoadAll().Select(n => new NoteViewModelJM(n)));
            NewCommand = new AsyncRelayCommand(NewNoteAsync);
            SelectNoteCommand = new AsyncRelayCommand<ViewModelsJM.NoteViewModelJM>(SelectNoteAsync);
        }

        private async Task NewNoteAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.NotePageJM));
        }

        private async Task SelectNoteAsync(ViewModelsJM.NoteViewModelJM note)
        {
            if (note != null)
                await Shell.Current.GoToAsync($"{nameof(Views.NotePageJM)}?load={note.Identifier}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                NoteViewModelJM matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note exists, delete it
                if (matchedNote != null)
                    AllNotes.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                NoteViewModelJM matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note is found, update it
                if (matchedNote != null)
                {
                    matchedNote.Reload();
                    AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
                }
                // If note isn't found, it's new; add it.
                else
                    AllNotes.Insert(0, new NoteViewModelJM(Models.Note.Load(noteId)));
            }
        }
    }
}
