# Brake Toy — doğrulama, derleme ve teslim rehberi

Taban: v0.9 Adventure; tarih 24 Ekim 2026. Bütün komutlar Unity/Git proje kökünden çalıştırılır.
Bu belge bir koşu raporu değildir; sonuç iddiası gerçek çıktı ve zamanla desteklenir.

## 1. Göreve göre en küçük yeterli kontrol

- Yalnız belge: satır sınırı, referanslar, kaynakla tutarlılık, git diff ve eşitleme geri okuması.
- Saf kural: ilgili EditMode/standalone kontrolü; davranış etkilendiğinde gerekli regresyon.
- Mekanik/giriş/havuz/kayıt: ilgili PlayMode testi, ardından paylaşılan alanların regresyonu.
- Model/ışık/UI: gerçek motor ön/arka/yakın/oyun kamerası; materyal/hacim/bağlantı denetimi.
- Android değişikliği: paket/imza/ABI/SDK ve uygun cihaz kontrolü.
- Sunucu: referans server testleri; Adventure ile protokol eşliği ayrıca kanıtlanmalıdır.
- Gereksiz tam derleme veya aynı yeşil testi tekrar tekrar koşma; yeni değişiklik/failure gerektiriyorsa genişlet.

## 2. Ortamı ve veriyi koru

1. Dal, HEAD, `git status --short`, Unity sürümü ve aktif sahneyi kaydet.
2. Unity aynı projede açıksa kaydedilmemiş kullanıcı işini koruyarak kapat; kilidi zorla kaldırma.
3. Testlerde ReviewSession.Persist=false, ArcadeDirector.PersistRecords=false kullan.
4. Kalite/ses/özellik testlerinde PersistPreferences=false; gerçek PlayerPrefs'i değiştirme.
5. Test kariyeri, token ve DB'yi geçici konumda tut; üretim kaydıyla deneme yapma.
6. Sonuç dosyasının bu koşuda üretildiğini, test zamanı/sonuç/başarısız sayısını kontrol et.
7. Önce/sonra diff al; generator/testin değiştirdiği dosyayı kullanıcı düzenlemesiyle karıştırma.

Önceki testler Tests/ServerParity JSON fixture'larını yeniden üretebilir.
Farkı incele; yalnız bu koşunun beklenmeyen üretilmiş değişikliğini geri al, kullanıcı işini sıfırlama.
Library/Temp/Logs/Builds ve yerel anahtar/DB dosyaları kaynak commit'ine girmez.

## 3. Unity EditMode ve PlayMode

Unity Test Runner ile ilgili testi seçebilir veya aşağıdaki Windows akışını kullanabilirsin.
`unityExe` yolunu kendi ortamında doğrula; kullanıcı profil yolunu projeye sabitleme.

```powershell
$unityExe = 'C:/Program Files/Unity/Hub/Editor/6000.3.26f1/Editor/Unity.exe'
$project = (Get-Location).Path
$reports = Join-Path $project 'Logs/AgentVerification'
New-Item -ItemType Directory -Force -Path $reports | Out-Null
foreach ($mode in @('EditMode', 'PlayMode')) {
    $xml = Join-Path $reports ($mode + '.xml')
    $log = Join-Path $reports ($mode + '.log')
    $argsForUnity = @('-batchmode', '-nographics', '-projectPath',
        ('"' + $project + '"'), '-runTests', '-testPlatform', $mode,
        '-testResults', ('"' + $xml + '"'), '-logFile', ('"' + $log + '"'))
    $run = Start-Process -FilePath $unityExe -ArgumentList $argsForUnity `
        -WindowStyle Hidden -Wait -PassThru
    if ($run.ExitCode -ne 0) { throw "Unity $mode başarısız: $($run.ExitCode)" }
    [xml]$result = Get-Content -Raw -LiteralPath $xml
    if ($result.'test-run'.failed -ne '0') { throw "$mode test hatası" }
}
```

- Test runner'a `-quit` ekleme; koşuyu erken sonlandırabilir.
- Hedefli koşu için `-testFilter BrakeToy.Tests.AdventureSceneTests` gibi tam sınıf adı kullan.
- Exit code tek başına yeterli değildir; XML result/failed, test sayısı ve log hatalarını incele.
- Render/ışık görsel kontrolü `-nographics` çıktısıyla doğrulanmaz.
- Saf çekirdek için isteğe bağlı .NET 8 SDK: `dotnet run --project Tests/Standalone/BrakeToy.CoreChecks.csproj`.
- Standalone akış bütün NUnit/PlayMode testlerinin yerine geçmez; güncel ortamda gerçekten koştuğunu belirt.

## 4. Mevcut kanıtın sınırı

- `UNITY_TEST_RESULTS-v09.json`: 50 EditMode +62 PlayMode =112; tarihi test kanıtıdır.
- Altı seed ×iki mod ×120 saniye:24 dakika simüle güvenli yol, sıfır havuz tükenmesi.
- 100 seed ×256 sıra seçimi: torba/hafıza/mesafe kuralları.
- Bu witness tehlikeli/riskli bütün yolların ve gerçek çocuk kullanımının kabulü değildir.
- Kayıtlı test sayısı hedef kota değildir; yeni test eklenirse sayı ve kanıt güncellenir.
- Yeni sonuç için yeni tarihli dosya kullan; eski XML/JSON'u bugünün koşusu gibi yeniden etiketleme.

## 5. Model ve gerçek görüntü denetimi

- Mevcut sahne: Tools → Brake Toy → Open Adventure v0.9; Game 9:16 ve 1×.
- `BrakeToy.Editor.AdventureValidation.Check`: mesh/index/finite vertex/material/shader/catalog denetimi.
- Bu tarama görünen bütün model birleşimlerinin kusursuz olduğu anlamına gelmez.
- `BrakeToy.Editor.AdventureCapture.Capture`:13 gerçek Unity görüntüsü; batchmode +D3D11 gerekir.
- Capture PlayMode'a girip kendi çıkışını yönetir; `-quit` veya `-nographics` ekleme.
- Capture mevcut Adventure PNG'lerini yazar; görsel baseline değişikliğini diff'te incele.
- Yakın plan yanında gerçek portre kamera, gündüz/gece, teker/taban, far/cam, halat/pivot ve rampa kenarı.
- Kadraj, dekor çakışması ve açık yol; model pozuyla aynı andaki analitik hacim birlikte kontrol edilir.
- Son kaynak görüntüleri `ADVENTURE_GALLERY.html`; sanat onayı kullanıcıdan ayrıca alınır.

## 6. Android üretimi ve paket kontrolü

- `BrakeToy.Editor.AdventureValidation.CheckAndBuild`: model denetimi ardından AndroidPrototypeBuild.
- Son paket `Builds/Android/BrakeToy-v0.9-Adventure.apk`; non-Development ARM 64/IL 2CPP.
- Build aracı sahneyi dosya varlığından seçer; logdan gerçekten Adventure üretildiğini doğrula.
- Yeni yayın/paket işi seçilirse versionName/versionCode hedefini belirle; belge işinde artırma.
- appId, sürüm, min/target SDK, debuggable ve izinleri aapt 2 ile kontrol et.
- apksigner ile imza ve önceki sertifika; zipalign `-c -P 16 -v 4` ile paket hizalaması.
- ZIP bütünlüğü, ARM 64 ELF LOAD segmentlerinin 16 KB hizalaması, byte sayısı ve SHA 256 kaydı.
- Aygıtta güncelleme kanıtı için gerçek kurulum gerekir; aynı sertifika tek başına fiziksel test değildir.
- Son v0.9: kod 10, minSDK 25, targetSDK 36; bunlar önceki paketin kanıtıdır, gelecek mağaza gereği değildir.
- `ANDROID_BUILD_REPORT-v09.json` test/debug sertifikasını açıkça belirtir; mağaza anahtarı değildir.
- Mağaza/SDK kuralları yayın aşamasında güncel resmî kaynaklarla doğrulanır.

## 7. Fiziksel cihaz kontrol listesi

- Önceki sürümden kurulum: TP/araç/güç hakkı korunması, açılış ve yeniden açılış.
- Temiz kurulum: bir kez başlangıç hakları; retry ve pause ile hak çoğaltma olmaması.
- Parmağı bırakma/yeniden tutma, UI başlangıcı, ikinci parmak güç/korna ve kenar sürüşü.
- Relax'ta yeni makete çarpışma; her can kaybı sonrası okunabilir ve adil devam.
- Her aile, rampa/cetvel iki kenarı, havada yıldız ve güç bitişi/iniş güvenliği.
- Pause/arka plan/odak/ekran oranı/safe-area/ses ve kalite ayarı.
- Yüksek/ekonomik profilde 10–15 dakika; FPS/p95, bellek ve ısı gözlemi.
- Rapor: sürüm/commit, cihaz/OS, profil, süre, seed, giriş türü, ölçüm yöntemi, hata adımları.
- Editör, otomatik witness ve fiziksel kullanıcı koşusunu etiketle; birini diğerinin yerine yazma.

## 8. Referans servis ve online doğrulama

- `Server/` içinde `python -m unittest -v test_server`; geçici DB ve sır olmayan örnek veriler.
- `Server/README.md` endpoint/protokol ve üretim sınırlarının ayrıntı kaynağıdır.
- ReviewNetworkProbe eski review-r1 istemci/servis entegrasyonudur; Adventure doğrulaması değildir.
- Canlı dağıtım/kimlik/ödeme açmak bağımsız ürün ve güvenlik kabulü gerektirir.

## 9. Teslim ve belge eşitleme

- Kaynak commit, test zamanı/sonucu, render/cihaz kanıtı ve açık maddeleri günlükte kaydet.
- GitHub'a ilgili dal/PR; APK gerekiyorsa Release; yüklenen varlığın size/digest değerini karşılaştır.
- Notion güncellemesinden önce sayfayı oku; kullanıcı eklerini ve child page bloklarını koru.
- Güncellemeden sonra içerik/başlık/bağlantıları geri oku; yalnız tool success'e dayanma.
- Kota hatasında aynı create işlemini döngüye sokma; mevcut sayfa güncellemesi mümkünse küçük düzeltme dene.
- Güncel eşitleme sonucu `Docs/NOTION_SYNC-agent-handbook.json`; tarihsel pending notlarını koru.
- Son rapor: ne değişti, nasıl doğrulandı, ne doğrulanmadı, GitHub ve Notion gerçek durumu.
