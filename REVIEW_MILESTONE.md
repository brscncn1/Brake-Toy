# v0.6 — Faz 6 kapsamına kadar revizyon adayı

Bu sürüm kalan sistemlerin birlikte denenebilmesi için hazırlanmıştır. Kullanıcının hedef sanat/oynanış kalitesini kabul ettiği anlamına gelmez. Mağazada yayımlanmış nihai ürün değildir.

## Eklenen çalışan kapsam

- Ayrı ToyRoomReview sahnesi, eski altı sahne ve varlıkların korunması.
- Merkezi Toy Points ekonomisi; sürümlü checksum/yedekli kariyer, tur ödülünün tekrar yazılmasını önleyen oturum ve son tur kimlikleri.
- 10 açılabilir araç varyantı, gerçek 3D dönen garaj, satın alma/seçim/kayıt. Ortak collider/kontrol/tekerlek sistemi; rekabet avantajı yok.
- Üç iz rengi ve üç korna; sahip olunan seçime ikinci kez ücret yok.
- Üç dönen kısa görev ve 15 tek ödüllü başarım.
- Relax: düşük hız/tavan, seyrek engel, üç hak; Arcade ve günlük rekorlarından ayrım.
- Ana menü, garaj, görevler, ayarlar, pause, sonuç, yerel rekor ekranları; daha büyük yazı seçeneği.
- Mod başına son parkur için en iyi sıkıştırılmış ghost; seed/mod kontrolü ve çarpışmasız şeffaf araç. 10 dakika sınırı, sınırlı dosya sayısı.
- Cihaz UTC gününe bağlı açıkça etiketlenmiş günlük antrenman.
- ILeaderboardService, Unity REST istemcisi ve sunucuda gerçek girdi simülasyonu. Sunucu UTC günlük turu, tek kullanımlık bilet, mesafe/stil ayrı top100/kendi/yakın sıralar. Bu APK'da online kapalı.

## Gerçek doğrulama

Unity 6000.3.26f1: **39 EditMode +36 PlayMode =75/75 geçti**. Python referans servis: **9/9 geçti**. Gerçek Unity HTTP probe: oturum → tur → girdi → sunucuda sonuç → kendi sıra → daily/style → kapalı bağlantıda Relax; **geçti**. Bunlar yerel döngü testidir; üretim hosting/online oyuncu kabulü değildir.

Gerçek Unity renderı: altı menü, on araç ve iki mod görüntüsü. İlk DX12 yakalama kapanışında native süreç hatası oluştu; kamera/RenderTexture temizliği düzeltildi ve D3D11 son yakalama **0 çıkış koduyla** tamamlandı. Bu hata Android uygulamada görüldü diye raporlanmıyor.

Model referans kontrolü: 316 renderer, 133 benzersiz mesh ve 768.322 üçgen; **tüm inaktif 10 araç ve havuz dahil**. Eksik mesh/materyal, shader hatası ve oda dekor collider'ı sıfır. Bu toplam aktif kare maliyeti veya cihaz FPS ölçümü değildir. MODEL_VALIDATION-review.json ayrıntıdır.

Android non-Development ARM64 IL2CPP APK başarıyla üretildi: **38.232.631 bayt**, 0.6.0/kod6, min API25/hedef36. SHA256: `65750071216c7742625eab7f8e410f027a815f96741d3518c6eecb6821ead042`. ZIP bütünlüğü, v2 imzası, ZIP 16 KB hizalaması ve altı native kütüphanenin ELF LOAD 16 KB hizalaması geçti. Tek Android izni INTERNET; boş endpoint nedeniyle otomatik istek yok. Android Debug sertifikası kullanılır; mağaza release imzası değildir. ANDROID_BUILD_REPORT-review.json ayrıntıdır. APK henüz tablette veya gerçek 16 KB ortamda çalıştırılmadı.

## Fazların dürüst durumu

| Alan | Bu aday | Kabul için açık kalan |
|---|---|---|
| Faz 1–3 temel/oynanış/görsel | Çalışan önceki temel korunuyor | Kullanıcının tempo, parkur derinliği, müzik ve sanat revizyonları |
| Faz 4 ilerleme/garaj/Relax | Yukarıdaki yerel sistemler uygulandı ve otomatik sınandı | Yeni adayın fiziksel tekrar giriş/garaj/kesinti testi; tam çok dil/screen reader uygulanmadı |
| Faz 5 online/daily/ghost | Ghost ve pratik günlük yerel; gerçek HTTP adapter ve referans sunucu yerelde doğrulandı | Canlı HTTPS, kalıcı kimlik/kurtarma, hedef yaş/izin uygunluğu ve Android üretim testi; UGS kurulmadı |
| Faz 6 yayın hazırlığı | Paket/test/render/izin-imza incelemesi ve yayın dokümanları | Düşük/orta cihaz matrisi, ısınma, yeni aday kabulü, sahibine ait release imzası/AAB/Console, mağaza varlıklarının son kabulü |
| Faz 7 genişleme | Bu teslim dışında | Ek odalar/harita olayları isteğe bağlı |

Reklam, IAP, tam yerelleştirme ve üretim online hesabı tamamlanmış gibi sunulmuyor. Mağaza finali için sadece hesap açılması yeterli değildir; yukarıdaki kalite/cihaz ve üretim çalışmaları da gereklidir.

## Benim eleştirim ve önerim

- On araç tek model ailesinden türetilmiş oran/renk/detay varyantlarıdır. On bağımsız profesyonel tasarım kalitesinde değiller. Sonraki sanat turunda özellikle Bubble Bug, Pixel Kart ve Crown Racer silüetleri ayrıştırılmalı.
- Büyük okunabilir menüler işlevsel, fakat düz paneller/arka plan ve garaj sunumu son ürün cilasında değil. Daha bütünlüklü sanat ve hareket dili gerekir.
- Kullanıcının belirttiği parkur sığlığı ve müzik eksikliği korunuyor. Kullanıcı bu değişiklikleri final sonrası revizyon turuna ayırdı; REVISION_BACKLOG.md öncelik kaydıdır.
- Yerel testler kısa doğrulama sağlar. Tam ayarda v0.5'in öznel akıcılığı, v0.6'nın uzun süreli bellek/ısı veya farklı telefon kabulü yerine geçmez.

## Kullanıcı işlemleri

1. Yeni APK'yı mevcut uygulamanın üzerine kur; Arcade/Relax, garajda seçim ve uygulamayı kapatıp açınca kayıt, pause/arka plan dönüşünü dene. PLAYER_GUIDE.md kısa kılavuzdur.
2. İmkân varsa iki profilde 60 sn ölçüm ve 10–15 dk ısınma denemesi; sayısal ölçüm yoksa gözlemini aynen kaydedelim.
3. Revizyon için önce zorluk/parkur, sonra görsel ve müzik hedeflerini somutlaştıracağız. Mevcut sürüm için hedef kaliteyi kabul etmen beklenmiyor.
4. Canlı online istersen hosting/sağlayıcı ve hedef yaş/bölge kararı gerekir. Şu anda Unity Cloud hesabı açman gerekmiyor; mevcut adapter UGS kullanmıyor. Server/README.md gerçek seçenek ve açık işleri gösterir.
5. Mağaza dağıtımı aşamasında yayıncı adı/iletişim/politika URL'si, Play Console ve kullanıcıya ait kalıcı imza anahtarı gerekir. Anahtar/parolaları sohbet veya repoya yazma; yerel güvenli kurulum yapılır. Bu test APK'sı mağazaya gönderilmez.
