# Oyuncu deneyimi paketleri — 27 Ekim 2026

Dal feat/player-experience-packages; taban ecdf702 (PR20). v0.10.0/kod11 Android adayı.

## Uygulananlar

- R1: Arcade önde; Relax/günlük/yerel rekor/güçler ikincil çekmecede.
  Yerel rekor ekranında bağlı olmayan online düğmeleri gizlendi.
  Genel, ses, görüntü ve ölçüm panelleri; ayrı geri dönüş alanları.
  Büyük yazıda720×1280 ve1600×2560 gerçek render/raycast geri dönüş kontrolü.
  HUD hedef/uyarı plakaları; kısa, güç harcatmayan ilk sürüş açıklaması.
  Tur hedefleri ve kariyer görevleri adları ayrıldı.
  Sonuçta BRAKE TOY; sıfır hava kazancında0TP, pozitif kazançta gerçek ek miktar.
  2500TP sürüş tavanı hesaba katılır; bu rakam toplam ödülün içindedir, tekrar eklenmez.
- R2: eski Index%6 yerine seed tabanlı, son3deseni tekrar etmeyen üretim.
  Üç ritim: geçit aralıkları16/18/20; Arcade cümleleri76/80/84birim.
  Rampa tek hava yıldızı, uzayan hava ödülü veya erken hava ödülü olarak çeşitlenir.
  Açık/alçak engelli ilk çıkış karışımı ve güçsüz bypass korunur.
  Her cümlede6engel; kitap/defter/kalemlik, otobüs ve bütün eski oyuncak türleri korunur.
  Bu bir tam fizik tabanlı serbest parkur üreticisi değildir; altı tarif hafızayla birleştirilir.
  Ghost tabletop-r6-grammar; eski dosyalar silinmez.
- R3: Rocket Pop/Thunder Volt/Galaxy Glider için kapalı sürekli gövde meshleri,
  farklı hacimler, gövdeye bağlı farlar, ortak teker pivotları ve ayrı yeni prefablar.
  Garaj ve sürüş aynı yeni varlıkları kullanır;10aracın teker/materyal/bounds denetimi.
  Saten boyalı masa örneği: hafif gren, farklı pürüzlülük ve daha açık renk.
  Ayarlar → Görüntü → Masa kaplamasını değiştir ile eski görünüm geri gelir.
  Bu sanat örneğidir; tüm oda/varlıkların nihai sanat kalitesi ve kullanıcı kabulü açık.
- R4 hazırlığı: otomatik Development benchmark artık Adventure üretimini izler.
  Awake sırasında kariyer/rekor/ses/grafik/güç tercih yazımları kapanır.
  Üretim APK'sında elle yerel60sn ölçümü vardır; otomatik test insan sürüşü değildir.

## Doğrulama ve sınırlar

96EditMode +91GPUPlayMode =187benzersiz test geçti.
Son model far yerleşimi ve iki oranlı geri dönüş denetimi için aynı3kabul testi tekrar3/3 geçti;
bu tekrar187toplamına ikinci kez eklenmedi. İlk UI koşusunda aynı karede yeniden ebeveynlenen
düğmenin eski raycast derinliği yakalandı; test insan etkileşimi gibi sonraki kareyi bekler.
100seed×100cümlede desen hafızası/uzunluk/rol; mevcut18güçsüz60sn geçişi ve rampa regresyonları geçti.
Gerçek kariyer/ghost dosyalarının önce/sonra hash'leri aynı.
Kanıt: Reports/EXPERIENCE_VALIDATION.json ve Reports/Visuals/EXPERIENCE_GALLERY.html.

Tablet şu an bağlanamıyor. Fiziksel dokunma/FPS/p95/ısı/arka plan kabulü YAPILMADI.
Föy: Docs/TABLET_ACCEPTANCE-v010.md. Yeni modellerin beğeni/kullanıcı kabulü de bekliyor.
K6 final yayın aşamasına geçilmedi. K2/K3 uygulaması ilerledi, K4 gerçek cihaz kabulü açık;
ardından K5 yayın kapsamı/ekonomi kararları ve K6 yayın adayı/mağaza hazırlığı kalıyor.
Çevrimdışı ilk yayın için kaba tahmin3–6haftalık odaklı çalışma; sanat ve cihaz kabulüne bağlı.
Bu süre otomatik çalışma taahhüdü veya mağaza incelemesi dahil kesin tarih değildir.
Online/IAP/yeni müzik bu çalışmada açılmadı. APK test imzalı adaydır, mağaza finali değildir.
Notion kotası nedeniyle kayıtlar DEV_LOG, NOTION_PENDING ve masaüstü yedeğinde tutulur.
