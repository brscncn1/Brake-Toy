# v0.7 Joyful Room — sonuç ve eleştiri

Bu sürüm kullanıcı revizyonudur; yeni sahne `Assets/BrakeToy/Scenes/ToyRoomJoyful.unity`. Önceki altı sahne korunur.

## 23 Ekim 2026 — v0.7 Joyful Room

Yeni sahne ToyRoomJoyful, dal feat/joyful-room-v07. Ayrı CC0 menü ve yarış müziği, gerçek plastik korna/oyuncak çarpma kaydı ve açıklamalı üç ses kanalı eklendi. Arcade hız 14→30; 10. saniyede yaklaşık 22.2, 30. saniyede 28.1. Son sekiz kalıbı hatırlayan seedli 32 kalıp ailesi; sürekli yatay ofset, oyuncak bebek/ayı/ördek/roket/robot, hareketli otobüs ve gelişen falsolu 1–3 top var. Yeşil oklu kitap rampaları 1.65 saniyelik uçuş ve yalnız havada kazanılan TP bölümüne 2x verir.

Mint oyun matı/kremsi masa, daha geniş oda kadrajı, radyo, oyuncak sandıkları, renkli halı, flama/gezegen süsü ve 120 saniyelik güneş/ay/yıldız/kuş döngüsü eklendi. Önceki sahneler korundu. TP/açılmış araçlar ilk yüklemede taşınır; yeni hız kuralları nedeniyle rekor, kariyer dosyası ve X+Y ghost sürümlendi. Eski review-r1 sunucuya yeni joyful-r2 skoru gönderilmez; canlı online zaten kurulmamıştı.

Son Unity koşusu 44 EditMode +43 PlayMode =87/87 geçti. Bu sonuç fiziksel cihaz FPS/ısınma veya insan ses/görsel kabulü değildir. Ayrıntılar MILESTONE-v07.md ve Tests/*-v07.xml. Android paket ve teslim kanıtları ANDROID_BUILD_REPORT-v07.json / DELIVERY-v07.json içinde ayrı kaydedilir.

## Oynanışın ölçülebilir değişimi

Arcade 14 hızla başlar ve 14 saniyelik zaman sabitiyle 30'a yaklaşır. Engel satırları 1.55–1.71 saniyelik ritimle gelir. İlk engel 21 m'de; ilk rampa ikinci satırdadır. 12 saniyeden sonra iki, 36 saniyeden sonra üç top karşılaşması açılır. Toplar ilerledikçe daha hızlı, daha geniş falsolu hareket eder; renk panelleri ve masa hareketine göre dönüş yuvarlanmayı görünür kılar. Otobüsün yatay hareketine tekerlek dönüşü eşlik eder. Relax 9→14, daha seyrek satır ve tek top kullanır.

32 kalıp ailesi, sekiz kalıplık yakın geçmiş, tür/stagger/yaw/jitter/ofset çeşitliliği ve yeni seed farklı turlar üretir. Sonsuz benzersizlik garantisi yoktur. Güvenli koridor ve zamanlı geçişler korunur; üç seed×iki modda 120 saniyelik gerçek runtime tanık sürüşü çarpışma/havuz tükenmesi olmadan geçti. Bu, her rastgele tur için matematiksel ispat değildir.

Rampa girişindeki yeşil ok izlenir; kalkış merkez toleransı 0.55 birim, uçuş 1.65 sn. Yeterli yükseklikteki araç alçak engelleri aşar; yüksek engel ve iniş anında çarpışma sürer. TP çarpanı yalnız havadaki mesafe/stil payına uygulanır; görev ve başarımların tamamı ikiye katlanmaz. Sonuç ekranı uçuş/TP bilgisini gösterir.

## Ses

Menü: Bobjt / leo's theme, işlenmiş 87.014 sn loop. Yarış: Zane Little Music / Pixel Sprinter, yazarın 34.286 sn loop'u. Durum değişiminde 0.65 sn eşit güç geçişi, DSP zamanlaması, başlangıç fade ve çarpışmada duck vardır. Korna gerçek plastik çocuk bisikleti kaydı; çarpışma rioforce plastik oyuncak dökülme kaydından işlenmiştir. Kaynaklar THIRD_PARTY_ASSETS.md ve Audio/Joyful/SOURCES.json.

Motor uğultu/lastik kanalını; Efektler çarpma, korna ve yıldızı; Müzik menü/yarışı yönetir. Genel ses bunları birlikte susturur. Dosyalar decode/peak/RMS/seam açısından ölçüldü; insan dinleme kabulü yapılmadı. Melodi tekrarının hiç fark edilmeyeceği garanti edilmez.

## Görsel inceleme ve açık eleştiri

16 gerçek 720×1280 Unity renderı üretildi; bunlar AI konsepti değildir. Gündüz/gece, menü/ayar, 12/42 sn sürüş, uçuş ve yedi oyuncak/rampa yakından incelendi. Uçuş pitch birikmesi, masa matının düzlük hissi, boş duvar, yatay otobüsün sabit tekerleri ve rampa kapağı hizası düzeltildi. Model vertex/collider uyumu, materyal/shader referansları ayrıca denetlenir.

Beğenmediğim kalan sonuç: oda hâlâ stilize ve aydınlatma fazla homojen; oyuncaklar önceye göre ayrıntılı olsa da film düzeyinde gerçekçi modelleme değil. Parkur uzun düz bir masa olarak okunuyor; 32 aile tekrar riskini azaltır ama tasarımcı üretimi özel parkurların derinliğine eşit değildir. Penceredeki dünya basit 3D dioramadır. Gün/gece okunur fakat sinematik atmosfer henüz hedef seviyesinde değil. Sonraki sanat revizyonunda daha güçlü malzeme ayrımı, kontrollü ışık/gölge ve kahraman model odaklı bir kabul karesi üzerinde kullanıcıyla uzlaşılması yararlı olur. Bu öneriler yeni kapsam onayı/kalite kabulü diye işaretlenmez.

## Testler ve kabul sınırı

- EditMode **44/44**, PlayMode **43/43**, toplam **87/87**. XML'ler Tests altında.
- 128000 plan satırında tekrar/erişilebilirlik; hız integrali/tersi; eğrisel top/bus temas koridoru; NaN reddi/yalnız hava TP; yeni ghost yükseklik serileştirmesi.
- Gerçek sahne/prefab geometrisi, dört portre oranı ve 1.5 sn ileri bölge; kalkış/iniş/pause pozu; üç seed×iki mod×120 sn; müzik/ayar bağlantısı; gün/gece ve dekor collider'ları.
- Fiziksel v0.7 tablet, sayısal FPS/p95/ısınma, uzun oturum, insan ses ve sanat onayı **bekler**. Önceki v0.5 kullanıcı geri bildirimi yeni sürümün performans kanıtı değildir.
- Eski v0.6 referans sunucusu korunur, joyful-r2 desteği yoktur; yeni scene online kapalıdır. Üretim servis/kimlik/kurtarma/mağaza kabulü tamamlanmadı.

## Kullanıcının yapacağı deneme

APK'yı önceki oyunu kaldırmadan güncelle veya Unity'de Tools → Brake Toy → Open Joyful Room v0.7 → Play. Menü/yarış müziğini, üç ses ayarını ve kornayı dinle; ilk 30 sn zorluk, 12/36 sn sonrası çoklu toplar, rampa/iniş ve yeni oda görünümünü değerlendir. Mümkünse tam grafikle 60 sn ölçüm, pause/arka plana dönüş ve yeniden başlatma dene. Veriyi silme veya uygulamayı kaldırma gerekmez.

## Son Android paket

Başarılı non-Development ARM64 IL2CPP paket: **43,206,769 bayt**, SHA256 `d5cd12dee2d4995c43d026a888c13e115c38c9aa2e264e3215343f88d978c0be`. v2 imza ve önceki test paketleriyle aynı sertifika, ZIP bütünlüğü/16KB hizalama, ELF LOAD16KB, paket/sürüm/portre/INTERNET kontrol edildi. Debug sertifikası mağaza release anahtarı değildir. Fiziksel v0.7 cihaz ve 16KB işletim ortamı testi yapılmadı. [APK](https://github.com/brscncn1/Brake-Toy/releases/tag/v0.7.0).
