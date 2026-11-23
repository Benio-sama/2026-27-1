using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Tobbretegu.Model;

namespace Tobbretegu.Repository
{
    public class NoteRepo
    {

        private static readonly string path = Path.Combine(
            Directory.CreateDirectory(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Tobbretegu")).FullName,
            "notes.xml");
        private static readonly object _syncLock = new object();

        public static NoteList LoadNotesFromFile()
        {
            string path = Path.Combine(
                Directory.CreateDirectory(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Tobbretegu")).FullName,
                "notes.xml");
            lock (_syncLock)
            {
                if (File.Exists(path))
                {
                    // read the file content
                    try
                    {
                        var serializer = new XmlSerializer(typeof(NoteList));
                        using (var fs = File.OpenRead(path))
                            return (NoteList)serializer.Deserialize(fs);
                    }
                    catch
                    {
                        // if the file is invalid, fall back to defaults
                    }
                }


                // provide default data for debugging
                return new NoteList()
                {
                    new Note()
                    {
                        ID = Guid.NewGuid(),
                        Title = "Otthoni teendők",
                        Description = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse efficitur enim a mauris gravida elementum. Fusce interdum fermentum nisi, sed tempor magna convallis eu. Suspendisse imperdiet accumsan pharetra. Cras vel nunc sit amet dui volutpat sodales. Sed in felis facilisis, interdum purus in, vestibulum neque. Suspendisse at magna cursus, pulvinar sem non, viverra mauris. Nulla egestas maximus sem, ultricies eleifend lorem pulvinar sit amet. Praesent tincidunt dapibus ante et mattis. Morbi vestibulum sollicitudin venenatis. Mauris finibus, dolor nec aliquet commodo, est ipsum pellentesque dui, et pretium felis est eu sapien.",
                        Assignee = Guid.Parse("87923C8F-17FC-4EDF-99D3-68568BFDB93B")
                    },
                    new Note()
                    {
                        ID = Guid.NewGuid(),
                        Title = "Házi feladatok",
                        Description = @"Phasellus ullamcorper nibh lorem, sit amet accumsan libero efficitur blandit. In non eros viverra, porttitor nunc a, finibus ante. Phasellus mattis leo in luctus accumsan. Nam in leo et nisi sodales posuere sed a purus.",
                        Assignee = Guid.Parse("8CCB7032-7099-4D70-AE50-26EEC9F8EE8E")
                    }
                };
            }
        }

        public static void SaveNotesToFile(NoteList allNotes)
        {
            string path = Path.Combine(
                Directory.CreateDirectory(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Tobbretegu")).FullName,
                "notes.xml");
            lock (_syncLock)
            {
                var serializer = new XmlSerializer(typeof(NoteList));
                using (var fs = File.Create(path))
                    serializer.Serialize(fs, allNotes);
            }
        }

    }
}
