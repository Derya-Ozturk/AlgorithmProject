# AlgoritmProject

## **Proje Amacı**

Bir şehirdeki etkinliklerin listesi verilmektedir. Her etkinlik için başlangıç zamanı, bitiş zamanı, mekan, ve önem derecesi bilgileri mevcuttur. Ek olarak mekanlar arası geçiş süreleri de dakika bazında verilmektedir. Amacımız, kullanıcının mekansal kısıtlamaları ve etkinliklerin önem derecelerini göz önünde bulundurarak, katılacağı etkinliklerden elde edeceği toplam değeri maksimize edecek bir program yazmaktır.

**Örnek Girdi:**
````
events = [
  {"id": 1, "start_time": "10:00", "end_time": "12:00", "location": "A", "priority": 50},
  {"id": 2, "start_time": "10:00", "end_time": "11:00", "location": "B", "priority": 30},
  {"id": 3, "start_time": "11:30", "end_time": "12:30", "location": "A", "priority": 40},
  {"id": 4, "start_time": "14:30", "end_time": "16:00", "location": "C", "priority": 70},
  {"id": 5, "start_time": "14:25", "end_time": "15:30", "location": "B", "priority": 60},
  {"id": 6, "start_time": "13:00", "end_time": "14:00", "location": "D", "priority": 80}
]

duration_between_locations_minutes_matix = [
	{ "from": "A", "to": "B", "duration_minutes": 15 },
	{ "from": "A", "to": "C", "duration_minutes": 20 },
	{ "from": "A", "to": "D", "duration_minutes": 10 },
	{ "from": "B", "to": "C", "duration_minutes": 5 },
	{ "from": "B", "to": "D", "duration_minutes": 25 },
	{ "from": "C", "to": "D", "duration_minutes": 25 },
]
````

**Beklenen Sonuç:**
````
Katılınabilecek Maksimum Etkinlik Sayısı: 3
Katılınabilecek Etkinliklerin ID'leri: 1, 6, 4
Toplam Değer: 200
````
