import sqlite3
def insertTousers (fname,lName,email) :
    try :
        conn = sqlite3.connect (r"D:\salintorn_python\test6.1.py")
        c = conn.cursor()

        sql = ''' INSERT INTO users (fname,lName,email) VALUES (NULL,"B"?,?)'''
        data = (fname,lName,email)
        c.execute(sql,data)
        conn.commit ()
        c.close ()

    except sqlite3.Error as e :
        print('Failend to insert : ',e)
    finally :

        if conn :
            conn.close ()

insertTousers()
insertTousers (

    import sqlite3

def insertTousers (fname,lName,email) :
    try :
        conn = sqlite3.connect (r"D:\salintorn_python\exmple.db")
        c = conn.cursor()

        sql = ''' INSERT INTO users (fname,lName,email) VALUES (?,?,?)'''
        data = (fname,lName,email)
        c.execute(sql,data)
        conn.commit ()
        c.close ()

    except sqlite3.Error as e :
        print('Failend to insert : ',e)
    finally :

        if conn :
            conn.close ()

insertTousers('pack','salintorn','salintorn.f@kkumail.com')
insertTousers ('pack','salintorn','abc')


conn = sqlite3.connect (r"D:\salintorn_python\exmple.db")
c = conn.cursor() 
c.execute('''INSERT INTO users (fname,lname,email,id) VALUES (NULL,"A","A","A")''')
c.execute('''INSERT INTO users VALUES (NULL,"B","B","B")''')
conn.commit () 
conn.close () 