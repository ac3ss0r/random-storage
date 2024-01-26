using System;
using LiteDB;
using System.Linq;

namespace LolzConsoleNotes {
    class Program {
        static void Main(string[] args) {
            using (var db = new LiteDatabase("base.db")) {
                var notes = db.GetCollection<Note>("notes");
                notes.EnsureIndex(x => x.Title, true);

                while (true) {
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("Консольные заметки для лолза.");

                    var allNotes = notes.FindAll();
                    Console.WriteLine($"Всего доступно {allNotes.Count()} заметок.");
                    Console.WriteLine("-----------------------------");

                    for (int i = 0; i < allNotes.Count(); i++) {
                        Console.WriteLine($"|#{i + 1}|{allNotes.ElementAt(i).Title}            |");
                    }

                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("Выберите опцию:\n" +
                                      "1. Просмотр существующей\n" +
                                      "2. Редактирование существующей\n" +
                                      "3. Создать новую заметку\n" +
                                      "4. Удалить заметку");

                    Console.Write("> ");
                    string opt = Console.ReadLine();

                    try {
                        if (opt == "1") {
                            Console.Write("Выберите номер заметки > ");
                            int note_id = int.Parse(Console.ReadLine());
                            var selectedNote = allNotes.ElementAt(note_id - 1);
                            Console.WriteLine($"{selectedNote.Title}: {selectedNote.Content}");
                        } else if (opt == "2") {
                            Console.Write("Выберите номер заметки > ");
                            int note_id = int.Parse(Console.ReadLine());
                            Console.Write("Введите новый текст заметки > ");
                            string data = Console.ReadLine();
                            var selectedNote = allNotes.ElementAt(note_id - 1);
                            selectedNote.Content = data;
                            notes.Update(selectedNote);
                            Console.WriteLine("Заметка успешно изменена.");
                        } else if (opt == "3") {
                            Console.Write("Введите название > ");
                            string title = Console.ReadLine();
                            Console.Write("Введите текст заметки > ");
                            string data = Console.ReadLine();
                            notes.Insert(new Note { Title = title, Content = data });
                            Console.WriteLine("Заметка успешно создана.");
                        } else if (opt == "4") {
                            Console.Write("Выберите номер заметки > ");
                            int note_id = int.Parse(Console.ReadLine());
                            var selectedNote = allNotes.ElementAt(note_id - 1);
                            notes.Delete(selectedNote.Id);
                            Console.WriteLine("Заметка успешно удалена");
                        }
                    } catch (Exception) {
                        Console.WriteLine("Выбранный элемент не существует.");
                    }

                    Console.WriteLine("Нажмите для возврата на главную.");
                    Console.ReadLine();
                }
            }
        }
    }
}