import sqlite3

dbConn = sqlite3.connect("base.db", check_same_thread=False)
dbCursor = dbConn.cursor()

def fetch_notes():
    return dbCursor.execute(f'SELECT * FROM notes').fetchall()

def delete_note(title):
    dbCursor.execute("DELETE FROM notes WHERE title = ?", (title,))
    dbConn.commit()

def commit_note(title, text):
    dbCursor.execute("INSERT OR REPLACE INTO notes(title, content) VALUES (?, ?)", \
                    (title, text))
    dbConn.commit()

def main():
    dbCursor.execute("CREATE TABLE IF NOT EXISTS notes(title TEXT PRIMARY KEY, content TEXT)")
    while True:
        notes = fetch_notes()
        print("-----------------------------")
        print("Консольные заметки для лолза.")
        print(f"Всего доступно {len(notes)} заметок.")
        print("-----------------------------")
        for i in range(0, len(notes)):
            print(f"|#{i+1}|{notes[i][0]}            |")
        print("-----------------------------")
        print("Выберите опцию:\n"
              "1. Просмотр существующей\n"
              "2. Редактирование существующей\n"
              "3. Создать новую заметку\n"
              "4. Удалить заметку")
            
        opt = input("> ")
        try:
            if opt == "1":
                note_id = int(input("Выберите номер заметки > "))
                print(f"{notes[note_id-1][0]}: {notes[note_id-1][1]}")
            elif opt == "2":
                note_id = int(input("Выберите номер заметки > "))
                data = input("Введите новый текст заметки > ")
                commit_note(notes[note_id-1][0], data)
                print("Заметка успешно изменена.")
            elif opt == "3":
                title = input("Введите название > ")
                data = input("Введите текст заметки > ")
                commit_note(title, data)
                print("Заметка успешно создана.")
            elif opt == "4":
                note_id = int(input("Выберите номер заметки > "))
                delete_note(notes[note_id-1][0])
                print("Заметка успешно удалена")
        except IndexError:
            print("Выбранный элемент не существует.")
        input("Нажмите для возврата на главную.")

if __name__ == "__main__":
    main()