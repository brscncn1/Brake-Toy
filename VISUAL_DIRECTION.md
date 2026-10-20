# Toy Room — Odanın içindeki oyuncak masası

21 Ekim 2026 kullanıcı geri bildirimi: İlk prototipte oda hissi eksik. Oyuncu çocuk odasının içini görebilmeli; Sparky odadaki masanın üzerinde ilerlemeli.

## Durum

Kullanıcı bu görsel yönü onayladı. `Concepts/ToyRoom-Tabletop-v1.png` hedef kompozisyon referansıdır. Görsel imagegen yerleşik aracıyla üretilmiştir; Unity ekran görüntüsü, uygulanmış sahne veya cihaz performansı taahhüdü değildir. v0.1 sahnesi/APK korundu; v0.2 için ayrı bir oynanabilir oda/masa sahnesi ve APK üretildi. Ana gereksinim kaynağı korunur; bu dosya yeni geri bildirimi kaydeder.

## Hedef kompozisyon

- Ön ve orta planda ahşap oyuncak masası; üzerinde serbest yatay sürüş. Asfalt, şerit veya yarış pisti görünümü yok.
- Çevrede masanın yüksekliğini anlatan kenar/ayak, aşağıda oda zemini; arkada pencere, perde, raf, yatak, halı ve odanın duvarları.
- Kamera arkadan ve yukarıdan bakar; Sparky ekranın alt bölümünde, yaklaşan engeller okunabilir. Mevcut 52 derece açı sabit bir nihai karar değildir; oda görünürlüğü ve tepki mesafesi birlikte test edilir.
- Kitap, blok kulesi, silgi ve tenis topu oyuncak ölçeğini anlatır. Merkezde yeterli güvenli geçiş alanı bırakılır. Dekorlar çarpışma tehlikesi gibi gösterilmez.
- Sıcak ışık, yumuşak gölgeler, oyuncak plastikleri ve okunabilir renkler. Referanstaki ince doku/ışık ayrıntısı mobilde sadeleştirilir; hedef orta sınıf Android'de ölçülecek gerçek zamanlı 3D'dir.

## Teknik tasarımda çözülmesi gerekenler

Masa yüzeyi kayan modüler parçalar, oda ise daha yavaş değişen çevre katmanı olarak ele alınmalı. Böylece her şey aynı hızda kayan bir duvar kâğıdı gibi görünmez. Sonsuz sürüşün görünür bir masa sonunda bitmesi veya kenardan düşüşe dönüşmesi istenmiyor; uzak masa sınırını gizleyen bir kadraj ve geçiş tasarımı gerekecek. Yeni düşme mekaniği önerilmiyor.

Odanın görünmesi için kamerayı fazla alçaltmak engel görüşünü azaltabilir. İlk görsel çalışmada farklı kamera açıları ve telefon oranları karşılaştırılmalı; sürüş alanı, oyuncu boyutu ve yaklaşık 1.5 saniyelik tepki hedefi korunmalı. Masa üzerinde neredeyiz hissi ilk modellerle sınanmalı.

Toy Room çocuk odasındaki oyuncak/oyun masası olarak ele alınabilir. İlerideki Desk Disaster, daha yoğun çalışma masası ve kırtasiye temasıyla ayrıştırılabilir; iki harita aynı içerik havuzunun kopyası olmamalı.

## Önerilen sonraki sürüm

Faz 2'nin büyük sistemlerinden önce küçük bir görsel çalışma: oda kabuğu, masa, temel mobilyalar, iki veya üç kamera seçeneği, geliştirilmiş Sparky ve engel ölçüleri. Önce Unity içinde tek bir iyi görünen oynanabilir kesit doğrulanmalı. Sonra engel üretimi, hızlanma, Near Miss ve kombo bu dünyaya eklenmeli. Bu öneri mevcut yedi fazın nihai kapsamını kaldırmaz.

## Sonraki geliştirmeler

1. Oda/masa/kamera ve ölçek hissi.
2. Adil ve çeşitli engel üretimi, hızlanma, mesafe ve stil puanı, Near Miss/kombo/Fever.
3. Yuvarlanan top, ön uyarılı çevre olayları, sesler, lastik hareketi/izleri, parçacıklar ve kısa komik çarpışmalar.
4. Garaj, aynı sürüş özelliklerine sahip 10 araç, Toy Points, kozmetikler, görevler, başarılar, Relax ve kayıt.
5. Uygunluk sonrasında sıralamalar, Daily Challenge/ghost; gerçek cihaz optimizasyonu ve yayın hazırlığı. Diğer haritalar ilk sürüm sonrasına kalır.

## Konsept üretim kaydı

Araç: yerleşik imagegen. Kullanılan tam istem: `Concepts/ToyRoom-Tabletop-v1-prompt.txt`. Çıktı: `Concepts/ToyRoom-Tabletop-v1.png`. Görselin amacı oda/masa/araç ilişkisini göstermektir; aynı ayrıntı düzeyinin mobilde aynı performansla çalışacağı henüz sınanmadı.

## Uygulanan ilk kesit — v0.2

Gerçek motor görüntüsü `Preview-v0.2.png`. 20° ve 28° karşılaştırılarak oda görünürlüğü için 20° seçildi. Onaylanan konseptin ince doku ve ışık ayrıntısı henüz nihai kaliteye ulaşmış değildir. Yeni sahnenin otomatik testleri ve APK derlemesi tamamlandı; fiziksel cihaz ve kullanıcı hissiyatı bekleniyor.

## v0.3 görsel ilerleme

Kullanıcı daha gerçekçi modeller istedi. Yeni diecast Sparky silueti, boya/cam/metal/kauçuk ayrımı; jant/lastik/ayna/far/tampon, kitap sayfa/kapak, silgi kılıfı ve dikişli top oluşturuldu. CC0 gerçek ahşap diffuse/normal, pencere açıklığı ve güneş/gölgesiz dolgu, kumaş perde/açık abajur ve temas gölgesi eklendi. Preview-v0.3.png ve Models-v0.3-*.png çalışan Unity renderlarıdır. Nihai kalite/konsept eşitliği iddia edilmez.
