class nisit:
    def __init__(self,name,sex,year,department,province):
        self.name = name
        self.sex = sex
        self.year = year
        self.department = department
        self.province = province

    def show(self):
        if self.sex == 'ชาย':
            print('สวัสดีครับ ผมชื่อ '+self.name +' เพศ '+self.sex+' นักศึกษาชั้นปีที่ '+self.year + ' สาขา '+self.department+' มาจากจังหวัด '+self.province)
        elif self.sex == 'หญิง':
            print('สวัสดีค่ะ ฉันชื่อ'+self.name +' เพศ '+self.sex+ ' นักศึกษาชั้นปีที่ '+self.year + ' สาขา '+self.department+' มากจากจังหวัด '+self.province)
        else:
            print('ERROR!')

    @classmethod
    def info(self):
        print('-'*20,'แนะนำตัว','-'*20)
        name = input('ชื่อ : ')
        sex = input('เพศ : ')
        year = input('ชั้นปี : ')
        department = input('สาขา : ')
        province = input('จังหวัด : ')
        return self(name,sex,year,department,province)

x = nisit.info()
x.show()