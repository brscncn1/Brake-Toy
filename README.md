# BRAKE TOY — Faz 1

**NO BRAKES. BIG TROUBLE.** Gerçek 3D, portre, tek parmak arcade sürüş oyunu.

## Durum

Faz 1 gerçek 3D prototipi Unity **6000.3.26f1** ile içe aktarıldı ve derlendi; ToyRoom sahnesi, prefablar, materyaller ve URP ayarları üretildi. **19 EditMode + 4 PlayMode testi geçti.** Gerçek Android cihaz testi ve performans ölçümü henüz yapılmadı.

Unity 6.3 LTS / C# / URP 17.3.0 hedeflenir. Proje sürüm dosyası resmî sürüm sayfasından doğrulanan **6000.3.26f1** sürümünü belirtir. Aynı 6.3 LTS serisinin yeni yaması kullanılabilir; Hub sürüm farklılığı sorarsa kurulu 6000.3 sürümünü seçin. 6.0 veya 6.2 seçmeyin.

## İlk kurulum — sırayla

1. [Unity Hub](https://unity.com/download) kurun. Unity hesabınızla giriş yapıp kullanımınıza uygun lisansı etkinleştirin. Hesap/lisans işlemlerini siz yapmalısınız.
2. **Installs → Install Editor → Unity 6.3 LTS** seçin. **Android Build Support**, onun altındaki **Android SDK & NDK Tools** ve **OpenJDK** seçeneklerini işaretleyin.
3. Bu README'nin bulunduğu **BrakeToy** klasörünü kullanın. Unity Hub'da **Projects → Add → Add project from disk** seçerek bu klasörü gösterin. Ayrı bir boş proje oluşturmayın; Assets klasörünü tek başına seçmeyin.
4. Projeyi açın. İlk seferde internet üzerinden Unity paketleri çözülür; tamamlanmasını bekleyin. Package Manager hatası varsa önce onu çözün. URP 17.3 editor sürümüne bağlı bir Unity paketidir.
5. İlk açılışta sahne üreticisi çalışır: gerçek `.unity`, `.prefab`, `.mat` ve `.asset` dosyalarını oluşturur. Otomatik açılmazsa **Tools → Brake Toy → Create Phase 1 Prototype** menüsünü seçin. Mevcut kirli sahne varsa önce kaydedin. Üretici mevcut `ToyRoom.unity` dosyasının üzerine yazmaz.
6. **Assets → BrakeToy → Scenes → ToyRoom** sahnesini açın. **Game** penceresinde oran listesinden **9:16** seçin; listede yoksa **+** ile 540 × 960 sabit çözünürlük ekleyin.
7. Editörün üstündeki **Play** düğmesine, ardından oyundaki **OYNA** düğmesine basın. Sol fare tuşunu basılı tutup sağa sola sürükleyin. Araba dokunduğunuz yere atlamaz. Parmağı/fareyi bırakınca yatay konumu korunur.
8. Bir engele çarpın; dünya durmalı ve **TEKRAR OYNA** görünmelidir. Yeniden başladığınızda araba ve parkur başlangıca dönmelidir. Sağ üstteki **II** ile duraklatılabilir.

Teslim edilen projede sahne ve prefablar zaten üretilmiştir. Üretici yalnızca sahne yoksa devreye girer; var olan çalışmayı değiştirmez. Elle nesne veya Inspector referansı bağlamak gerekmez. `Preview.png`, Unity Play Mode sahnesinden alınmış 3D önizlemedir.

## Ayarları değiştirme

Üretimden sonra `Assets/BrakeToy/ScriptableObjects/Phase1Config.asset` seçin. Inspector'da:

| Alan | Varsayılan | Etki |
|---|---:|---|
| Track Half Width | 3.5 | Zemindeki kullanılabilir alanın yarısı |
| Player Half Width / Length | 0.39 / 0.70 | Sürekli çarpışma hesabı; prefab collider'ıyla birlikte değiştirilmeli |
| World Speed | 8 | Saniyede dünya kayma mesafesi |
| Drag Sensitivity | 9 | Bir ekran genişliği sürüklemenin dünya karşılığı |
| Max Lateral Speed | 9.5 | Saniyede maksimum yatay hareket |
| Player Viewport Y | 0.25 | Araç merkezinin alttan ekran oranı |
| Camera Pitch / Vertical Field Of View | 52 / 50 | Kamera açısı / dikey görüş açısı |
| Target Frame Rate | 60 | Hedef; ölçülmüş cihaz performansı değildir |

Kamera/konfigürasyon değişikliklerini Play Mode'u yeniden başlatarak kontrol edin. Play Mode içinde Inspector değişiklikleri genelde kalıcı olmaz. Engel collider'ı kök nesnede, ölçek 1 ve dönüş 0 olmalıdır. Faz 1 çarpışma algoritması XZ düzlemindeki sabit, eksen hizalı kutular içindir.

## Testler

**Çalıştırılmış test:** Aynı üretim C# dosyaları .NET 8 ile derlenerek 19 çekirdek kontrolü geçti; bunlardan biri sabit seed ile 10.000 çarpışma örneğini bağımsız geometri hesabıyla karşılaştırır. Ayrıntı: `TEST_RESULTS.json` ve `TEST_REPORT.md`.

**Unity testleri çalıştırıldı ve geçti. Yeniden çalıştırmak için:**

1. **Window → General → Test Runner → EditMode → Run All**: 19 çekirdek kontrolü.
2. Üretilmiş ToyRoom sahnesi build sahne listesine eklenmişken **PlayMode → Run All**: 4 test; başlangıç/çarpışma/tekrar/duraklatma, farklı portre oranlarında kamera, mouse/UI filtresi ve dokunma/ikinci parmak.
3. Test sırasında oyuna elle dokunmayın; PlayMode çarpışma testi ortadaki ilk engele düz sürüş yapar.

Unity dışı tekrar (makinede .NET 8 SDK olmalı):

```powershell
dotnet run --project Tests/Standalone/BrakeToy.CoreChecks.csproj -- TEST_RESULTS.json
```

Bu bilgisayarda test için indirilen SDK proje klasörünün dışındaki çalışma alanında tutuldu; Unity oyununa bağımlılık eklemez. Unity kullanmak için ayrıca .NET SDK kurmanız gerekmez.

## Hazır Android APK

`Builds/Android/BrakeToy-Phase1.apk` başarıyla derlendi: **ARM64 / IL2CPP / Development**, yaklaşık **34.5 MiB**. Paket kimliği `com.braketoy.prototype`, sürüm 0.1.0; minimum API 25, hedef API 36. APK imzası ve arşiv bütünlüğü doğrulandı. Dosya özeti/SHA-256: `ANDROID_BUILD_REPORT.json`.

Bu test APK'sıdır; fiziksel telefonda henüz çalıştırılmadı ve mağaza yayınına hazır değildir. Android 7.1+ ARM64 cihazda APK'yı açıp yükleme iznini onaylayarak denenebilir. Alternatif olarak aşağıdaki USB Build And Run adımlarını kullanın. Kamera/mikrofon/konum izni içermez; Development bağlantısı için INTERNET izni vardır.

Yeniden üretmek için **Tools → Brake Toy → Build Android Prototype** menüsü de kullanılabilir.

## Android denemesi

1. **File → Build Profiles → Android → Switch Platform**. İlk kurulumda modüller eksikse Hub → Installs → ilgili sürüm → Add modules.
2. **Edit → Project Settings → Player → Other Settings → Active Input Handling** değeri **Input System Package (New)** olmalı. Değiştirilirse Editor yeniden başlatması istenebilir.
3. Portre yönü, ARM64 ve IL2CPP sahne üreticisinde ayarlanır. `com.braketoy.prototype` geçici paket kimliğidir; mağaza yayınına hazır kimlik değildir.
4. Telefonda geliştirici seçenekleri ve USB hata ayıklamasını açın; USB ile bağlayın ve cihazdaki bilgisayara güven sorusunu onaylayın.
5. Android Build Profile'da cihazı seçip **Build And Run** kullanın. APK'yı `Builds/Android/BrakeToy.apk` gibi bir konuma kaydedin.
6. Gerçek dokunma, parmağı bırakıp yeniden dokunma, çoklu dokunma, bildirim/arka plan sonrası devam, ekran kesiti ve uzun sürüşü `TEST_PLAN.md` ile doğrulayın.

iOS uyumlu giriş ve yönlendirme tasarlanmıştır; iOS derlemesi/testi yapılmadı. iOS son derleme ve imzalama için macOS/Xcode gerekir.

## Dosyalar ve sınırlar

- `Assets/BrakeToy/Scripts/Core`: Unity'den bağımsız kontrol/çarpışma matematiği.
- `Assets/BrakeToy/Scripts/Runtime`: giriş, oyuncu, dünya, kamera, oyun durumu ve arayüz.
- `Assets/BrakeToy/Editor/PrototypeBuilder.cs`: sahne, materyal, prefab, URP ve oyuncu ayarlarını oluşturan editör aracı.
- `Assets/BrakeToy/Tests`: Unity testleri ve ortak çekirdek kontrolleri.
- `SOURCE_REQUIREMENTS.md`: kullanıcının ana promptunun eksiksiz kopyası.
- `GAME_DESIGN.md`, `ARCHITECTURE.md`, `ROADMAP.md`, `CHANGELOG.md`, `TEST_PLAN.md`: kapsam, mimari, sıradaki işler.

Reklam, analiz, ödeme, hesap veya çevrimiçi servis SDK'sı eklenmedi. Faz 2 ve sonraki sistemler uygulanmış değildir. Unity 3D önizlemesi incelendi. Gerçek cihazda 60 FPS, mobil kontrol hissi ve nihai görsel kalite henüz onaylanmadı.

## Resmî teknik başvurular

- [Unity 6000.3.26f1 sürümü](https://unity.com/releases/editor/whats-new/6000.3.26f1)
- [Unity 6.3 URP proje kurulumu](https://docs.unity3d.com/6000.3/Documentation/Manual/urp/creating-a-new-project-with-urp.html)
- [Input System 1.17 dokunmatik desteği](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.17/manual/Touch.html)
- [Unity'nin resmî 6.3 URP paket tanımı](https://github.com/Unity-Technologies/Graphics/blob/6000.3/staging/Packages/com.unity.render-pipelines.universal/package.json)

## Git

Klasör kendi yerel Git deposu olarak hazırlanmıştır; uzak sunucuya gönderilmez. Unity ilk içe aktarmayı ve sahne üretimini tamamladıktan sonra oluşan asset ve `.meta` dosyalarını birlikte kaydedin. `Library`, yerel kullanıcı ayarları ve derleme çıktıları hariç tutulur. Düzenlenmiş sahne/prefab dosyalarını üretici üzerinden silerek yenilemeyin.
