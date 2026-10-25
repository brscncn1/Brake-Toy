# K1.2 — GitHub'dan temiz klon doğrulaması

Tarih: 25 Ekim 2026. Belge dalı `chore/clean-clone-verification`.
Doğrulanan oyun/araç kaynağı: `df4ba16189dc482b8800f839b51e95cac8c74f76` (PR #14).

## Kopyanın kaynağı ve sınır

- Kaynak GitHub brscncn1/Brake-Toy, dal fix/explicit-android-build-target.
- Remote'dan shallow clone alındı; başlangıçta HEAD doğrulandı ve git status temizdi.
- Başlangıçta Library yoktu; önceki projenin Assets/Library/Builds/ayarları kopyalanmadı.
- Çalışma klasörü Codex çalışma alanında ayrı BrakeToyClean-df4ba16 dizinidir.
- Bu klasör normal klondur; ana projenin dalı veya kullanıcının açık dosyaları değiştirilmez.
- Unity 6000.3.26f1 kurulu ve lisanslı aynı makine kullanıldı. Global UPM/Unity paket
  önbelleği kullanılabilir; temiz işletim sistemi veya tamamen çevrimdışı kurulum testi değildir.
- Packages manifest/lock dosyaları Git kaynağından gelir. İlk import Library'yi yeniden üretir.

## Yeniden çalıştırma

Mevcut klasörün üzerine clone/import yapma; yeni boş bir yol seç.

```powershell
git -c core.longpaths=true -c core.autocrlf=false clone --depth 1 --single-branch `
  --branch fix/explicit-android-build-target https://github.com/brscncn1/Brake-Toy.git $cleanPath
Set-Location $cleanPath
git rev-parse HEAD
git status --short
Test-Path Library
```

Bu raporu birebir tekrar etmek için HEAD yukarıdaki df4ba16 kaynağı olmalı;
dal ilerlediyse sonuç yeni kaynak için ayrı kaydedilir. main'in aynı kodu içerdiğini varsayma.
İçe aktarmadan önce git temiz, Library kontrolü false olmalı.

- Unity exe/project yollarını DOGRULAMA.md örneğine göre seç; EditMode ardından PlayMode koş.
- EditMode -nographics olabilir; UI/garaj içeren PlayMode -force-d3d11 ile GPU açık çalışır.
- `-runTests` ile `-quit` kullanma. XML failed/passed/result, test sayısı ve log çıkışını kontrol et.
- XML/logları Reports/Local altında tut; kalıcı kanıtları ancak koşu sonrası seç.
- `dotnet run --project Tests/Standalone/BrakeToy.CoreChecks.csproj -- Reports/Local/Core.json`
  için .NET 8 hedefini derleyebilen SDK ve .NET 8 runtime gerekir.
- Burada SDK 10.0.402 ve runtime 8.0.21 ile 19/19 bağımsız kontrol geçti.
- Server klasöründe `python -m unittest -v test_server`: Python 3.14 ile 9/9 geçti;
  referans servisi için pip paketi veya bulut hesabı gerekmedi.
- AndroidPrototypeBuild.Validate, APK üretmeden hedef planını kontrol eder; kullanım
  `ANDROID_BUILD_PROFILES.md` içindedir.

## Sonuç ve kanıt

74/74 EditMode +GPU açık 71/71 PlayMode =145/145. İlk -nographics PlayMode
koşusu 70/71 idi; UI/garaj testi URP RenderTexture hatası verdi. Aynı kaynak GPU
açıkken geçti. Bu bulgu nedeniyle test rehberi düzeltildi; oyun kodu değişmedi.
Üç otomatik biçim farkı doğrulanıp geri alındı; klon git temiz ve fsck başarılı.

- Nihai Unity test sayıları ve XML hashleri `../Reports/CLEAN_CLONE_VALIDATION.json` içinde.
- Bağımsız C# ve referans sunucu testleri Unity testleriyle aynı sayı altında toplanmaz.
- Testler gerçek kariyer/PlayerPrefs yazmayı kapatır; yeni dosyalı migration testleri geçici dizin kullanır.
- Import/testin oluşturduğu git farkları ayrıca kaydedilir; sessizce kullanıcı değişikliği sayılmaz.
- Test sonucundan görsel sanat kalitesi, fiziksel FPS/p95/ısı veya tablet kabulü çıkarılmaz.
- Gerçek Android APK/IL2CPP/imza/kurulum bu pakette çalıştırılmadı; SDK/NDK/JDK
  modülleri ve paket kontrolleri gerçek Android üretiminde ayrıca gerekir.
- Bu belge ve kanıt ekleri test edilen df4ba16 kaynak kodunu değiştirmez.
- Notion tekrar denenmedi; masaüstü BrakeToy_Notion_Yedek.txt aktarım yedeği güncellenir.

## Sonraki paket

K1 teknik doğrulamasından sonra K2.1: ilk 10/30/60 saniyede karar yoğunluğu ve
karşılaşma tekrarını ölçmek; sonuçlara göre tek bir oynanış çeşitliliği paketi seçmek.
K1'in cihaz/sanat/yayın kabulü ayrı olarak açık tutulur.
