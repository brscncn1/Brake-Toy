# Brake Toy — ürün ve teknik karar kaydı

Derleme tarihi: 24 Ekim 2026. Kararların asıl kaynağı kullanıcı talepleri ve ilgili teslim kodudur.
Bu dosya geçmiş konuşmayı tekrar okutmayı azaltır; yeni kullanıcı kararı önce gelir.
Durumlar: Kabul / Uygulandı / Doğrulama bekliyor / Açık / Öneri.

## 1. Geçerli kararlar

- D01 — Kabul: Android öncelikli portre, gerçek 3D Unity/URP, çocuk odasında masa üstü oyuncak sürüşü.
- Gerekçe: oda ölçeği ve canlı oyuncak dünyası ürün kimliğinin parçasıdır.
- D02 — Uygulandı: göreli serbest yatay sürükleme; ana ilerleme otomatik, fren yok.
- Etki: dokunma/mouse sürekliliğini ve UI üzerinde başlangıç filtresini koru.
- D03 — Kabul: geniş oda kadrajı, renkli dünya, gerçek motor referansıyla yüksek görsel kalite hedefi.
- Durum: maketler ve ışık iyileştirmeleri mevcut; nihai sanat kabulü açık.
- D04 — Kabul: ayrıntılı model çalışması daha sonra farklı agent desteğiyle ele alınabilir.
- Etki: model devir notu, ölçek/pivot ve analitik hacim sözleşmesi korunur.
- D05 — 26 Ekim değiştirildi: kalıcı yeşil kaçış şeridi kaldırılır; nadir kısa çıkış işareti kalabilir.
- Etki: geçit boşluğu değişir; güçsüz geçilebilirlik korunur. Kullanıcı mevcut öğeleri azaltmayı onaylamadı.
- Kitap/defter/kalemlik ve hareketli otobüs yeni oyuncaklara eklenir; fiili üretimleri ayrıca test edilir.
- D06 — Uygulandı: kitap rampaları farklı yatay konumlarda; kenar teması ve havada TP ek payı.
- Etki: merkezden geçme şartı geri getirilmez; yan giriş/çıkış ayrıca sınanır.
- D07 — Uygulandı: domino v0.8 sonrası kullanıcı isteğiyle aktif Tidy/Adventure'den kaldırıldı.
- Etki: eski Playground kodunda bulunması yeni sürüme tekrar ekleme kararı değildir.
- D08 — Uygulandı: radyo masa dışındaki sol rafa taşındı; kenar süsleri ortak 64 faza alındı.
- Etki: dekor/notebook/pencil-cup örtüşmesi ve radyo desteği regresyon testine bağlıdır.
- D09 — Kabul: mevcut müzik şimdilik korunur; yeni tercih için işitilebilir örnek üzerinden karar alınır.
- D10 — Uygulandı: yavaşlatma 3 sn, korumalı roket 3 sn, jetpack 5 sn; ilk kariyere üçer hak.
- Etki: retry stok vermez; kalıcı tüketim ve kayıt hatası geri alımı korunur.
- D11 — Uygulandı: TP ile güç yenileme 350/500/650; gerçek para ve premium coin uygulanmadı.
- Açık: kullanıcının geçmiş ücretli coin fikrinin sağlayıcı/yaş/mağaza kapsamı kesinleşmedi.
- D12 — Uygulandı: sekiz yeni parkur ailesi ve son üç aileyi hatırlayan torba üretimi.
- Etki: bütün ailelerin kullanıcı tarafından son sanat/oynanış kabulü verilmiş sayılmaz.
- D13 — Kabul: son yerleşim/mekanik paketlerinde temel hız eğrisi korunmuştu.
- Etki: yeni denge işi seçilirse ölçümlü revizyon yapılabilir; kalıcı değiştirilemez değer değildir.
- D14 — Uygulandı: eski sahneler ve kariyer korunur; v0.9 PlayerPrefs/ghost adları ayrı.
- Açık: ortak CareerData nedeniyle tam rekor ayrımı ayrıca test edilmelidir.
- D15 — Uygulandı: Adventure online bağlantısı kapalı; referans servis yalnız review-r1.
- Etki: backend hazır/UGS kurulmuş diye sunulmaz; yeni kural protokolü olmadan etkinleştirilmez.
- D16 — Kabul: otomatik kanıt, cihaz ölçümü, kullanıcı onayı ve mağaza kabulü farklı durumlardır.
- D17 — Kabul: tamamlanan çalışma GitHub'a; anlamlı belgeler Notion'a eşitlenir.
- Etki: kaynak dalı ve APK commit'i açık yazılır; eşitleme başarısızsa pending kaydı tutulur.
- D18 — Kabul: her agent belgesi en fazla 300 satır; görev bazlı okuma ve küçük iş paketleri.
- Etki: tüm oyunu tek turda bitirme hedefi konmaz; seçilen paket tamamlanır ve devir bırakılır.

## 2. Henüz verilmemiş kararlar

- O01: İlk mağaza yayını offline mı; hangi online özellikler zorunlu?
- O02: Kalıcı oyuncu kimliği/hesap kurtarma ve servis sağlayıcısı kim olacak?
- O03: Yardımlı/yardımsız ve yeni/eski kurallı rekorlar hangi politikalara göre ayrılacak?
- O04: Gerçek ödeme veya reklam ilk yayında olacak mı; çocuk hedefiyle akış nasıl ele alınacak?
- O05: Onaylı sanat referansı, model üreticisi, varlık başına bütçe ve kalite hedefi.
- O06: Düşük/orta cihaz listesi; sürdürülebilir FPS/p95/bellek eşikleri.
- O07: Çok dil, erişilebilirlik, ses/görsel yardım seçeneklerinin ilk yayın kapsamı.
- O08: Yeni oda/harita ve sosyal özelliklerin yayın sonrası önceliği.
- Bunlar varsayılan onay değildir; ilgili aşamada somut seçenek ve etkileriyle sahibine sunulur.
- Kullanıcı zaten açık karar verdiyse aynı onayı tekrar isteme; kaydı ve ilgili belgeleri güncelle.

## 3. Doğrulama adayları / kesinleşmemiş bulgular

- Q01: 25 Ekim testle doğrulandı ve düzeltildi; Relax yeni oyuncak/prop/rampa temizliği eklendi.
- Q02: 25 Ekim testle doğrulandı ve düzeltildi; ayrı Adventure kariyeri ve gerçek stil anahtarı kullanılır.
- Q03: PerformanceProbe benchmark'ında eski ArcadeSequence ile Adventure koşusu eşleşmesi incelenecek.
- Her aday için yeniden üretim, beklenen/gerçek davranış ve hedefli test gerekir.
- Q01/Q02 kanıtı Docs/HARDENING-2026-10-25.md; Q03 hâlâ inceleme adayıdır.

## 4. Yeni karar şablonu

```text
Kimlik / tarih / durum:
Problem ve kullanıcı beklentisi:
Seçenekler ve tercih:
Gerekçe / maliyet / geri alınabilirlik:
Etkilenen kod, belge ve kayıt:
Doğrulama ve kabul:
Yerine geçtiği karar / açık kalan konu:
```

Tarihsel kaynaklar: SOURCE_REQUIREMENTS.md, MILESTONE-v07/v08/v081/v09.md ve DEV_LOG.md.
Güncel davranış ayrıntısı: MEKANIKLER.md; geliştirme sırası: YOL_HARITASI.md.
