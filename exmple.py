import sqlite3
conn = sqlite3.connect(r'D:\salintorn_python\exmple.db')
c = conn.cursor() 
c = conn.cursor()
c.execute('''CREATE TABLE users (id integer PRIMARY KEY AUTOINCREMENT,
    fname varchar(30) NOT NULL,
    laname varchar(30) NOT NULL,
    email varchhar(100) NOT NULL)''')
c.execute('''INSERT INTO users (id,fname,lname,email) VALUES (NULL,'A','A','A')''')
conn.commit()
conn.close()
c.execute('''INSERT INTO users (fname,lname,email,id) VALUES (NULL,"A","A","A")''')
c.execute('''INSERT INTO users VALUES (NULL,"B","B","B")''')
conn.commit () 
conn.close () 