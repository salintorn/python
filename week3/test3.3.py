print("ป้อนชื่ออาหารสุดโปรดของคุณ หรือ exit เพื่อออกจากโปรดแกรม")
foodlist = []
a = 0
while(True) :
    a +=1
    fav = input("อาหารโปรดอันดับที่ {} คือ \t".format(a))
    foodlist.append(fav)
    if fav == 'exit':
        break
print("อาหารสุดโปรดของคุณมีดังนี ",end=" ")
for x in range(1,a) :
        print(x,".",foodlist[x-1],end=" ")

