a=[]
while True:
    b = input('----ร้านบิวตี้----\n เพิ่ม [a] \n แสดง [s]\n ออกระบบ [x]\n')
    b = b.lower()
    if b=='a':
        c = input('ป้อนรายกาลูกค้า(รหัส:ชื่อ:จังหวัด)')
        a.append(c)
        print('\n**********ข้อมลได้เข้าสู่ระบบแล้ว*********\n')
    elif b =='s':
        print('{0:-<6}{0:-<10}{0:-<10}'.format(""))
        print('{0:-<8}{1:-<10}{2:10}'.format('รหัส','ชื่อ','จังหวัด'))
        print('{0:-<6}{0:-<10}{0:-<10}'.format(""))
        for d in a:
            e = d.split(":")
            print('{0[0]:<6} {0[1]:<10}({0[2]:<10})'.format(e))
            continue
    elif b =='x':
        break
print('ทำคำสั่งถัดไป')
