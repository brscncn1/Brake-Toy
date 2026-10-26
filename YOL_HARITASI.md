# Brake Toy — güncel geliştirme yol haritası


27 Ekim güncel paket: feat/player-experience-packages; v0.10.0/kod11 aday kaynak.
HUD/ayar/sonuç, tabletop-r6 hafızalı parkur, Experience art varyantları;187benzersiz Unity testi.
Ayrıntı Docs/PLAYER_EXPERIENCE_PACKAGES-2026-10-27.md. Tablet/sanat kabulü açık; K6 başlamadı.
Önceki APK/sonuç ifadeleri tarihsel tabandır; yeni Android teslim kanıtı ayrı rapordadır.

Plan tarihi: 24 Ekim 2026. Taban v0.9.0 Adventure; kanıt `Reports/DELIVERY-v09.json`.
Bu plan bundan sonraki kalite/yayın çalışmasını tarif eder; eski Faz 1–6 kayıtları tarihseldir.
Seçili paketin dışındaki öneriler topluca uygulama yetkisi sayılmaz.

## 1. Başlangıç ve öncelik

- Hazır: sürüş, sekiz yeni oyuncak ailesi, rampalar, güçler, görev/garaj, kayıt ve yerel ghost.
- Son kanıt: K2 parkur revizyonunda165/165 Unity testi ve6motor karesi; temiz klon yeniden koşulmadı.
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
kanıt `Reports/ORGANIZATION_MANIFEST.json`. K1.3/K1.4 sağlamlaştırması bu dalda eklendi.
Öneri sırası: sağlamlaştırma → parkur/kombo → ışık/malzeme → araç parçaları → online.
Docker, yeni fiziğe dayalı olaylar, adaptif müzik ve parça kilitleri ayrı paketlerdir;
bu dosya düzenlemesinde uygulanmış sayılmaz.

Tahmin: 3–5 işgünü. Bağımlılık: doğru kaynak tabanı ve oyuncu verisinin korunması.
25 Ekim sağlamlaştırma paketi K1.3/K1.4 hatalarını yeniden üretti ve düzeltti;
diğer maddeler tamamlanmış sayılmaz. Ayrıntı: `Docs/HARDENING-2026-10-25.md`.

- K1.1: yedi agent belgesi, okuma sırası,300 satır sınırı, GitHub/Notion eşleştirmesi.
- K1.2 tamam: GitHub df4ba16 temiz klonu, taze import/derleme ve 145/145 Unity testi.
  C# 19/19, sunucu 9/9 ayrı geçti; Docs/CLEAN_CLONE-2026-10-25.md. Android APK bu turda derlenmedi.
- K1.3: Relax'ta yeni oyuncakla çarpışma sonrası aynı engelden peş peşe hak kaybını araştır.
- K1.4: paylaşılan kariyer ile v0.9 rekor anahtarlarının ayrımını test et; migration kararı çıkar.
- K1.5: güç bitişi, iniş alanı, rampa kenarı, cetvel yan çıkışı ve retry sınırlarını doğrula.
- K1.5 sonucu: jet alçalması ve cetvel yüzey gecikmesi düzeltildi; hedefli 5/5 geçti.
  Ayrıntı `Docs/LANDING-2026-10-25.md`; nihai kanıt `Reports/LANDING_VALIDATION.json`.
- K1.6: yoğun tek satır koddan bir alan seç; davranışı değiştirmeden okunabilir hâle getir.
- K1.7: dosya varlığına göre sahne seçen build aracında açık hedef seçimi ihtiyacını değerlendir.
- K1.6/K1.7: derleme zinciri okunabilir plan/profil yapısına ayrıldı; eski CheckAndBuild hedefleri düzeltildi.
  Ayrıntı `Docs/ANDROID_BUILD_PROFILES.md`; gerçek Android derlemesi bu pakette yapılmadı.
- K1.3/K1.4 yeniden üretim: düzeltme öncesi iki test başarısız, düzeltme sonrası ikisi de geçti.

Çıkış: kritik veri kaybı/crash yok, baseline tekrarlanabilir, şüpheli durumlar testle sonuçlanmış.
Kanıt: XML, örnek kayıt/migration, hata adımları, kaynak commit ve günlük.
K2.1 tabanı ve K2.3/K2.4 iki ailelik bitiriş deneyi tamamlandı; hız eğrisi korundu.
26 Ekim kullanıcı revizyonu: K2.4 masa eşyaları/otobüs geri dönüşü ve değişen geçitler önceliklidir.
Docs/TABLETOP_COURSE-2026-10-26.md: mevcut öğeleri azaltmadan yeni oyuncaklarla birleştir.
Uygulama/motor doğrulaması165/165 geçti; kullanıcı yoğunluk/tekrar/zorluk kabulü bekleniyor.
Bu K2 revizyonudur; son planlı K6 aşamasına henüz geçilmedi.
Tamamlanan kullanıcı paketi: yerinde dönen top hatası, dört hareket profili ve gece iki loş lamba.
Kapsam Docs/ROLLING_AND_NIGHT-2026-10-26.md; masa eşyaları/otobüs azaltılmaz, K2 devam eder.
Son revizyon: oda içi aydınlığı sabit, rampa ilk çıkışları açık/engelli karışık;179/179 motor testi geçti.
Oyuncu incelemesi Docs/FRESH_EYES_REVIEW-2026-10-26.md; önerilen sonraki paket HUD/menü okunabilirliği.
Bu öneriler topluca uygulanmadı; K2 ve kullanıcı kabulü sürer, K6 son aşamasına geçilmedi.
K1 teknik kaynak doğrulaması yapıldı; cihaz/sanat/Android paket kabulü ayrı açık kalır.

## 4. K2 — ilk dakika ve risk/ödül

Tahmin: 3–5 işgünü. Bağımlılık: K1 kontrol/çarpışma güvenilirliği.

- K2.1: ilk 10/30/60 saniyeyi kaydet; anlamlı karar, boş bekleme ve okunmayan tehlikeyi say.
- K2.1 sayısal taban: 100 seed/mod; Arcade 4/11/22, Relax 2/7/15 cümle referansı.
  Merkez↔kenar ritmi %100; Docs/OPENING_PACE-2026-10-25.md. İnsan karar/uyarı kabulü açık.
- K2.2: Arcade 14→30, Relax 9→14 hızlarını ve en az 48 birim karşılaşma aralığını birlikte ölç.
- K2.3: hızla karar yoğunluğunu ayrı ayarla; tüm parametreleri tek deneyde değiştirme.
- K2.4: sekiz ailenin giriş/çıkış ve birleşim çeşitlerini artır; son üç aile hafızasını koru.
- K2.5: yaklaşma, uyarı, kaçış, ödül ve inişte aynı okunabilirlik dilini kullan.
- K2.6: değişen geçitlerin güçsüz tanığına rampa/risk yollarını deneyen hedefli testler ekle.
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

Bu son planlı aşamaya başlarken kullanıcıya özellikle haber ver.

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
