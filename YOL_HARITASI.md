# Brake Toy — güncel geliştirme yol haritası

Plan tarihi: 24 Ekim 2026. Taban v0.9.0 Adventure; kanıt `Reports/DELIVERY-v09.json`.
Bu plan bundan sonraki kalite/yayın çalışmasını tarif eder; eski Faz 1–6 kayıtları tarihseldir.
Seçili paketin dışındaki öneriler topluca uygulama yetkisi sayılmaz.

## 1. Başlangıç ve öncelik

- Hazır: sürüş, sekiz yeni oyuncak ailesi, rampalar, güçler, görev/garaj, kayıt ve yerel ghost.
- Kayıtlı otomatik kanıt: 112/112 Unity testi ve 13 render; bu belge turunda yeniden koşulmadı.
- Açık: güncel cihaz ölçümleri, sanat/oynanış kabulü, kayıt sınırları ve mekaniklerin uç durumları.
- Canlı online, ödeme, üretim kimliği ve mağaza yayını tamamlanmadı.
- Hedef: çocukların okuyabildiği, ilk dakikada ilgi çeken, adil, akıcı ve tutarlı mobil oyun.
- Sıra: veri kaybı/crash → kontrol/çarpışma → tempo → sanat → cihaz optimizasyonu → yayın.
- Yeni engel eklemek, mevcut ailelerin kalitesini doğrulamanın yerine geçmez.

## 2. Zaman ve iş paketi sözleşmesi

- Süreler bir kişinin odaklı işgünü tahminidir; kesin teslim tarihi değildir.
- Model üretimi, cihaz erişimi, kullanıcı onayı ve mağaza inceleme beklemeleri takvime ayrıca eklenir.
- K1 sonunda gerçek iş yüküyle tahmini yenile; bütün fazların bitişini tek tarihe bağlama.
- Her paket yaklaşık 1–3 odaklı oturuma bölünür; uzun paket ölçülebilir alt teslimlere ayrılır.
- Aynı anda bir ana paket; ayrı agent varsa dosya sahipliği ve entegrasyon sırası yazılır.
- Paket biçimi: neden → kapsam → bağımlılık → uygulama → test → kabul kanıtı → devir.
- Durumlar: Öneri / Hazır / Sürüyor / Doğrulama bekliyor / Kabul edildi / Engelli.
- Kodun var olması “Kabul edildi” değildir; mevcut sonucu ve hedef sonucu ayrı işaretle.
- Yeni revizyonu etki/süre/bağımlılıkla ekle; önceki kararı sessizce silme.

## 3. K1 — taban, belgeler ve kritik sağlamlaştırma

25 Ekim ek paketi: rapor/arşiv düzeni ve Editor çıktı yolları düzenlendi;
kanıt `Reports/ORGANIZATION_MANIFEST.json`. Sonraki paket K1.3'tür.
Öneri sırası: sağlamlaştırma → parkur/kombo → ışık/malzeme → araç parçaları → online.
Docker, yeni fiziğe dayalı olaylar, adaptif müzik ve parça kilitleri ayrı paketlerdir;
bu dosya düzenlemesinde uygulanmış sayılmaz.

Tahmin: 3–5 işgünü. Bağımlılık: doğru kaynak tabanı ve oyuncu verisinin korunması.
Bu tur belge paketidir; aşağıdaki kod düzeltmeleri henüz yapılmış sayılmaz.

- K1.1: yedi agent belgesi, okuma sırası,300 satır sınırı, GitHub/Notion eşleştirmesi.
- K1.2: temiz klonda v0.9 test/derleme akışını yeniden üret ve ortamı kaydet.
- K1.3: Relax'ta yeni oyuncakla çarpışma sonrası aynı engelden peş peşe hak kaybını araştır.
- K1.4: paylaşılan kariyer ile v0.9 rekor anahtarlarının ayrımını test et; migration kararı çıkar.
- K1.5: güç bitişi, iniş alanı, rampa kenarı, cetvel yan çıkışı ve retry sınırlarını doğrula.
- K1.6: yoğun tek satır koddan bir alan seç; davranışı değiştirmeden okunabilir hâle getir.
- K1.7: dosya varlığına göre sahne seçen build aracında açık hedef seçimi ihtiyacını değerlendir.
- K1.3/K1.4 tarama bulgusudur; yeniden üretmeden kesin hata veya çözülmüş madde sayma.

Çıkış: kritik veri kaybı/crash yok, baseline tekrarlanabilir, şüpheli durumlar testle sonuçlanmış.
Kanıt: XML, örnek kayıt/migration, hata adımları, kaynak commit ve günlük.
Belge turundan sonraki önerilen tek paket: K1.3 için izole Relax çarpışma testi.

## 4. K2 — ilk dakika ve risk/ödül

Tahmin: 3–5 işgünü. Bağımlılık: K1 kontrol/çarpışma güvenilirliği.

- K2.1: ilk 10/30/60 saniyeyi kaydet; anlamlı karar, boş bekleme ve okunmayan tehlikeyi say.
- K2.2: Arcade 14→30, Relax 9→14 hızlarını ve en az 48 birim karşılaşma aralığını birlikte ölç.
- K2.3: hızla karar yoğunluğunu ayrı ayarla; tüm parametreleri tek deneyde değiştirme.
- K2.4: sekiz ailenin giriş/çıkış ve birleşim çeşitlerini artır; son üç aile hafızasını koru.
- K2.5: yaklaşma, uyarı, kaçış, ödül ve inişte aynı okunabilirlik dilini kullan.
- K2.6: yeşil yol witness'ına riskli yolları deneyen hedefli testler ekle.
- K2.7: güç olmadan geçilebilir parkur; stok harcamayı adaletsiz bölümün zorunlu çözümü yapma.
- K2.8: çocuk/ebeveyn ile kısa kullanım oturumları; açıklama gerektiren noktaları kaydet.

Çıkış: kullanıcı ilk dakika temposunu kabul eder; kaçış ve risk ödülü gözle anlaşılır.
Kanıt: önce/sonra video, seed/ayar karşılaştırması, kaçış testleri ve kullanıcı geri bildirimi.
Sayısal oyun deneyimi eşiklerini ilk ölçümden sonra belirle; tutunma oranı uydurma.

## 5. K3 — görsel referans ve model kalitesi

Tahmin: 8–15 işgünü; model üreticisinin kapasitesi süreyi değiştirir.
Bağımlılık: model/pivot/çarpışma bağlantı sözleşmesi ve kullanıcı görsel seçimi.

- K3.1: gerçek portre kameradan masa, araba, iki engel, oda ve ışık içeren örnek sahne.
- K3.2: kullanıcı motor içi referansı kabul etmeden bütün varlıkları yeniden üretme.
- K3.3: palet, ölçek, plastik/kumaş/ahşap/metal ayrımı ve gölge standardını kaydet.
- K3.4: araçlarda belirgin siluet; cam/far/teker bağlantısı ve oranları kontrol et.
- K3.5: aileleri sırayla değiştir; Moving/Options/Link/Shadow ve Solids bağlarını koru.
- K3.6: rampa/cetvel teması, tavan, tren, vinç askısı ve misket dönüşünü yakın planda izle.
- K3.7: radyo raf desteği,64 birim kenar döngüsü, defter/kalemlik boşluğu ve oda kadrajı.
- K3.8: menü/garaj/görevlerde ortak tipografi, boşluk, kontrast ve görsel hiyerarşi.
- K3.9: gün/gece atmosferi sürüş okunabilirliğini azaltmamalı.
- K3.10: modelin üçgen/materyal/ekran maliyetini cihaz bütçesine göre sınırla; lisansı kaydet.

Çıkış: onaylı referansla tutarlı modeller, eşleşen hacimler ve tamamlanmış kaynak/lisans kaydı.
Kanıt: gündüz/gece, ön/arka/yakın plan render, prefab ölçüleri ve kullanıcı sanat kabulü.
Konsept görseli ve gerçek motor görüntüsünü ayrı etiketle.

## 6. K4 — Android cihaz kabulü ve optimizasyon

Tahmin: 5–8 işgünü. Bağımlılık: aday sanat grubu ve erişilebilir hedef cihazlar.

- K4.1: Huawei MatePad 11'de güncel APK; önceki sürümden güncelleme ve temiz kurulum ayrımı.
- K4.2: yüksek/ekonomik profilde 10–15 dakika; FPS, p95, bellek ve ısınma/pil gözlemi.
- K4.3: benchmark witness'ının AdventureSequence kullandığını doğrula; eski generator yanlış rota oynatmasın.
- K4.4: mümkünse düşük/orta Android telefon, farklı aspect ve safe-area kontrolleri.
- K4.5: pause, odak kaybı, arka plan dönüşü, retry, çoklu dokunma, ses/kalite ayarları.
- K4.6: CPU/GPU/GC darboğazını ölç; havuz/gölge/shader/draw-call/overdraw düzeltmelerini ayır.
- K4.7: başlangıç hedefi yüksek 60, ekonomik 30 FPS; desteklenen cihazla birlikte kesinleştir.
- K4.8: desteklenen cihaz ve bilinen sınırlamaları açıkça yaz.

Çıkış: ölçülmüş sürdürülebilir akıcılık; kontrol/kesinti/kayıt senaryoları kabul edilmiş.
Kanıt: cihaz/OS/build/profile/süre içeren rapor. Editör FPS'si yerine geçmez.

## 7. K5 — ürün kapsamı, servis ve ekonomi kararı

Çevrimdışı kapsam kararı 1–2 işgünü; canlı online seçilirse ek 5–10+ işgünü başlangıç tahmini.
Bağımlılık: sahibin yayın kapsamı, yaş ve sağlayıcı kararı; güncel fiyat/uygunluk araştırması.

- K5.1: ilk yayın çevrimdışı mı, online sıralamalı mı; ödeme ilk yayında olacak mı?
- K5.2: yardımlı/yardımsız skor, modlar ve eski rekorların taşıma politikasını kesinleştir.
- K5.3: TP/başlangıç güç dengesi; çocuk deneyiminde anlaşılır yenileme ve harcama akışı.
- K5.4: online seçilirse Adventure için sürümlü istemci/sunucu simülasyon sözleşmesi.
- K5.5: kalıcı kimlik/kurtarma, HTTPS, silme/yedekleme/izleme, kesinti ve kötüye kullanım testleri.
- K5.6: günlük parkur için sunucu UTC'si; yerel antrenmanı canlı rekabet diye sunma.
- K5.7: ödeme seçilirse sağlayıcı/mağaza/yaş uygunluğu ve geri yükleme doğrulaması.
- K5.8: dış karar beklenirken bağımsız çevrimdışı kalite işleri ilerleyebilir.

Çıkış: ilk yayın kapsamı yazılı; seçilen servisler cihazda doğrulanmış veya kapsam dışında.
Ödeme/online kendiliğinden açılmaz; `review-r1` Adventure için hazır backend değildir.

## 8. K6 — yayın adayı ve devir

Tahmin: 3–5 işgünü; mağaza incelemesi hariç. Bağımlılık: K1–K5 seçili kapsamın kabulü.

- K6.1: özellik dondurma; yalnız kritik hata ve açık kabul maddeleri.
- K6.2: temiz klondan aday üret; ilgili regresyon ve cihaz testlerini tamamla.
- K6.3: yayın anahtarı, versionCode, AAB ve mağaza ayarları sahibin mevcut yetkisiyle yönetilir.
- K6.4: gizlilik/veri güvenliği/yaş/içerik beyanlarını gerçek SDK/servislerle eşleştir.
- K6.5: ikon, ekran görüntüsü, tanıtım ve destek kanallarını gerçek oyuna göre hazırla.
- K6.6: iç/kapalı test, geri bildirim, geri dönüş planı ve sürüm notları.
- K6.7: owner yayın onayı ve mağaza kabulü; yayını yalnız gerçek durumla raporla.

Çıkış: imzalı, ölçülmüş, kabul edilmiş ve dağıtımı doğrulanmış ürün adayı.
Yeni oda/harita, sosyal sistem ve yeni aileler ayrı backlog'dur; ilk yayına otomatik eklenmez.

## 9. İş paketi/devir şablonu

```text
Kimlik / durum / sorumlu:
Amaç ve oyuncuya etkisi:
Dahil dosyalar / kapsam dışı:
Bağımlılıklar / varsayımlar / süre:
Uygulama ve karar:
Çalıştırılan kontrol / gerçek sonuç / kanıt:
Kullanıcı veya cihaz kabulü gereken bölüm:
Commit / PR / Notion durumu:
Sonraki tek paket:
```

Süre doldu diye tamamlandı işaretleme; hata kanıtını ve kalan işi devir kaydına bırak.
