# Oyuncu deneyimi paketleri â€” 27 Ekim 2026

Dal feat/player-experience-packages; taban ecdf702 (PR20). v0.10.0/kod11 Android adayÄ±.

## Uygulananlar

- R1: Arcade Ã¶nde; Relax/gÃ¼nlÃ¼k/yerel rekor/gÃ¼Ã§ler ikincil Ã§ekmecede.
  Yerel rekor ekranÄ±nda baÄŸlÄ± olmayan online dÃ¼ÄŸmeleri gizlendi.
  Genel, ses, gÃ¶rÃ¼ntÃ¼ ve Ã¶lÃ§Ã¼m panelleri; ayrÄ± geri dÃ¶nÃ¼ÅŸ alanlarÄ±.
  BÃ¼yÃ¼k yazÄ±da720Ã—1280 ve1600Ã—2560 gerÃ§ek render/raycast geri dÃ¶nÃ¼ÅŸ kontrolÃ¼.
  HUD hedef/uyarÄ± plakalarÄ±; kÄ±sa, gÃ¼Ã§ harcatmayan ilk sÃ¼rÃ¼ÅŸ aÃ§Ä±klamasÄ±.
  Tur hedefleri ve kariyer gÃ¶revleri adlarÄ± ayrÄ±ldÄ±.
  SonuÃ§ta BRAKE TOY; sÄ±fÄ±r hava kazancÄ±nda0TP, pozitif kazanÃ§ta gerÃ§ek ek miktar.
  2500TP sÃ¼rÃ¼ÅŸ tavanÄ± hesaba katÄ±lÄ±r; bu rakam toplam Ã¶dÃ¼lÃ¼n iÃ§indedir, tekrar eklenmez.
- R2: eski Index%6 yerine seed tabanlÄ±, son3deseni tekrar etmeyen Ã¼retim.
  ÃœÃ§ ritim: geÃ§it aralÄ±klarÄ±16/18/20; Arcade cÃ¼mleleri76/80/84birim.
  Rampa tek hava yÄ±ldÄ±zÄ±, uzayan hava Ã¶dÃ¼lÃ¼ veya erken hava Ã¶dÃ¼lÃ¼ olarak Ã§eÅŸitlenir.
  AÃ§Ä±k/alÃ§ak engelli ilk Ã§Ä±kÄ±ÅŸ karÄ±ÅŸÄ±mÄ± ve gÃ¼Ã§sÃ¼z bypass korunur.
  Her cÃ¼mlede6engel; kitap/defter/kalemlik, otobÃ¼s ve bÃ¼tÃ¼n eski oyuncak tÃ¼rleri korunur.
  Bu bir tam fizik tabanlÄ± serbest parkur Ã¼reticisi deÄŸildir; altÄ± tarif hafÄ±zayla birleÅŸtirilir.
  Ghost tabletop-r6-grammar; eski dosyalar silinmez.
- R3: Rocket Pop/Thunder Volt/Galaxy Glider iÃ§in kapalÄ± sÃ¼rekli gÃ¶vde meshleri,
  farklÄ± hacimler, gÃ¶vdeye baÄŸlÄ± farlar, ortak teker pivotlarÄ± ve ayrÄ± yeni prefablar.
  Garaj ve sÃ¼rÃ¼ÅŸ aynÄ± yeni varlÄ±klarÄ± kullanÄ±r;10aracÄ±n teker/materyal/bounds denetimi.
  Saten boyalÄ± masa Ã¶rneÄŸi: hafif gren, farklÄ± pÃ¼rÃ¼zlÃ¼lÃ¼k ve daha aÃ§Ä±k renk.
  Ayarlar â†’ GÃ¶rÃ¼ntÃ¼ â†’ Masa kaplamasÄ±nÄ± deÄŸiÅŸtir ile eski gÃ¶rÃ¼nÃ¼m geri gelir.
  Bu sanat Ã¶rneÄŸidir; tÃ¼m oda/varlÄ±klarÄ±n nihai sanat kalitesi ve kullanÄ±cÄ± kabulÃ¼ aÃ§Ä±k.
- R4 hazÄ±rlÄ±ÄŸÄ±: otomatik Development benchmark artÄ±k Adventure Ã¼retimini izler.
  Awake sÄ±rasÄ±nda kariyer/rekor/ses/grafik/gÃ¼Ã§ tercih yazÄ±mlarÄ± kapanÄ±r.
  Ãœretim APK'sÄ±nda elle yerel60sn Ã¶lÃ§Ã¼mÃ¼ vardÄ±r; otomatik test insan sÃ¼rÃ¼ÅŸÃ¼ deÄŸildir.

## DoÄŸrulama ve sÄ±nÄ±rlar

96EditMode +91GPUPlayMode =187benzersiz test geÃ§ti.
Son model far yerleÅŸimi ve iki oranlÄ± geri dÃ¶nÃ¼ÅŸ denetimi iÃ§in aynÄ±3kabul testi tekrar3/3 geÃ§ti;
bu tekrar187toplamÄ±na ikinci kez eklenmedi. Ä°lk UI koÅŸusunda aynÄ± karede yeniden ebeveynlenen
dÃ¼ÄŸmenin eski raycast derinliÄŸi yakalandÄ±; test insan etkileÅŸimi gibi sonraki kareyi bekler.
100seedÃ—100cÃ¼mlede desen hafÄ±zasÄ±/uzunluk/rol; mevcut18gÃ¼Ã§sÃ¼z60sn geÃ§iÅŸi ve rampa regresyonlarÄ± geÃ§ti.
GerÃ§ek kariyer/ghost dosyalarÄ±nÄ±n Ã¶nce/sonra hash'leri aynÄ±.
KanÄ±t: Reports/EXPERIENCE_VALIDATION.json ve Reports/Visuals/EXPERIENCE_GALLERY.html.

Tablet ÅŸu an baÄŸlanamÄ±yor. Fiziksel dokunma/FPS/p95/Ä±sÄ±/arka plan kabulÃ¼ YAPILMADI.
FÃ¶y: Docs/TABLET_ACCEPTANCE-v010.md. Yeni modellerin beÄŸeni/kullanÄ±cÄ± kabulÃ¼ de bekliyor.
K6 final yayÄ±n aÅŸamasÄ±na geÃ§ilmedi. K2/K3 uygulamasÄ± ilerledi, K4 gerÃ§ek cihaz kabulÃ¼ aÃ§Ä±k;
ardÄ±ndan K5 yayÄ±n kapsamÄ±/ekonomi kararlarÄ± ve K6 yayÄ±n adayÄ±/maÄŸaza hazÄ±rlÄ±ÄŸÄ± kalÄ±yor.
Ã‡evrimdÄ±ÅŸÄ± ilk yayÄ±n iÃ§in kaba tahmin3â€“6haftalÄ±k odaklÄ± Ã§alÄ±ÅŸma; sanat ve cihaz kabulÃ¼ne baÄŸlÄ±.
Bu sÃ¼re otomatik Ã§alÄ±ÅŸma taahhÃ¼dÃ¼ veya maÄŸaza incelemesi dahil kesin tarih deÄŸildir.
Online/IAP/yeni mÃ¼zik bu Ã§alÄ±ÅŸmada aÃ§Ä±lmadÄ±. APK test imzalÄ± adaydÄ±r, maÄŸaza finali deÄŸildir.
Notion kotasÄ± nedeniyle kayÄ±tlar DEV_LOG, NOTION_PENDING ve masaÃ¼stÃ¼ yedeÄŸinde tutulur.

## Android teslimi

Derleme başarılı: v0.10.0/kod11, ARM64/IL2CPP, Development kapalı, test sertifikası doğrulandı.
Kaynak commit2f2d3f41ad530d90516d55a5cf61c28bc5018139; PR21.
APK40.863.403bayt; SHA256:
3b0a3c8c57df60cec57a0c7e1d95ef17484b8f98dccd8f9ead53f52da3f88bb5
GitHub varlık digest'i yerel dosyayla aynı.
[İnceleme adayı](https://github.com/brscncn1/Brake-Toy/releases/tag/v0.10.0)
ve Reports/EXPERIENCE_DELIVERY.json teslim kanıtıdır. Tablete yüklenmedi; cihaz bağlı değil.
