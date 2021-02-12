#dictionary loop word4.2
dictionary = []
pra = []
kum = []
i = 1
def menu():
    global Choice 
    print('\nพจนานุกรม\n1)  เพิ่มคำศัพท์\n2) แสดงคำศัพท์\n3) ลบคำศัพท์\n4) ออกจากโปรแกรม')
    Choice = input('Input Choice :')

def puss():
    dictionary.insert(0,input("เพิ่มคำศัพท์"))
    pra.insert(0,input("ชนิดคำศัพท์(n. ,v. ,adj. ,adv. :)"))
    kum.insert(0,input("ความหมาย"))
    print("เพิ่มคำศัพท์เรียบร้อยแล้ว")
def show():
    print('--------------------\nคำศัพท์ม่ีทั้งหมด ',len(dictionary),"\n----------------------")
    print('คำศัพท์ ประเภท ควมหมาย')
    x = 0
    while x < len(dictionary) :
        print(dictionary[x],' ',pra[x]," ",kum[x])
        x = x+1

def delete():
    x =str(input("พิมพ์คำศัพท์ที่ต้องการลบ:"))
    x2 =str(input("ต้องการลบ "+x+"ใช่หรือไม่ (y/n) :"))
    if x2 == "y":
        z=0
        while z < len(dictionary):
            if x == dictionary[z]:
                del pra[z]
                del kum[z]
            z = z+1
            dictionary.remove(x)
            print("ลบ"+x+"เรียบร้อย")
    else:
        print("ยกเลิกการลบคำศัพท์")  

while True:
    menu()
    if Choice == '1':
        puss()
    elif Choice  =='2':
        show()
    elif Choice=='3':
        delete()
    else:
        o =str(input("ต้องการออกจากโปรแกรมใช่หรือไม่ (y/n) :"))
        if o == "y":
            print("ออกจากโปรแกรมเรีอยร้อยแล้ว")
            break
        else:
            print("กลับเข้าสู่โปรแกรม")