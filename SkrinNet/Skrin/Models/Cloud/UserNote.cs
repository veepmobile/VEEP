using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Cloud
{
    public class UserNote
    {
        public UserData user_data { get; set; }
        public UserNoteData note_data { get; set; }

        public UserNote(UserNoteContainer input)
        {
            user_data = new UserData(input);
            note_data = new UserNoteData
            {
                content = input.content
            };
        }

        public UserNote()
        {

        }
    }


    public class UserNoteData
    {
        public string content { get; set; }
    }

    public class UserNoteContainer : UserDataContainer
    {
        public string content { get; set; }

        public UserNoteContainer()
        {

        }

        public UserNoteContainer(UserNote note)
            : base(note.user_data)
        {
            content = note.note_data.content;
        }
    }


}