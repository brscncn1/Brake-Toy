# Android derleme profilleri

25 Ekim 2026; K1.6/K1.7. Dal `fix/explicit-android-build-target`, taban PR #13.

## Sorun ve davranış

Önceki AndroidPrototypeBuild en yeni mevcut sahneyi dosya varlığı zinciriyle seçiyordu.
Tidy/Joyful vb. CheckAndBuild kendi modelini denetleyip Adventure APK'sı üretebiliyordu.
Eksik sahnede de PrototypeBuilder çağrısı vardı; hedefin eksik olması gizlenebiliyordu.

- AndroidBuildPlan hedef, sahne, aday APK yolu ve Development seçimini birlikte tutar.
- Varsayılan hedef açıkça Adventure'dır. Dosya varlığı hangi sürümü seçeceğini belirlemez.
- Eksik veya tanınmayan hedef hata verir; eski sahneye düşmez ve sahne üretmez.
- Altı CheckAndBuild yöntemi kendi profilini BuildProfile'a doğrudan verir.
- Profiller: Adventure, Tidy, Playground, Joyful, Review, Living, Refined, Arcade,
  Tabletop, Prototype. İsimler büyük/küçük harfe duyarsız; sayısal enum değerleri kabul edilmez.
- İlk beş profil non-Development; diğer tarihsel sahneler Development davranışını korur.
- APK hedefi ARM64 / IL2CPP'dir. Build sırasında değiştirilen backend/architecture/AAB
  tercihleri finally içinde eski değerlerine döner; bu kurtarma gerçek APK koşusuyla henüz sınanmadı.
- Eski yayımlanmış APK yollarına yazılmaz. Yeni aday yolu:
  `Builds/Android/Candidates/BrakeToy-Adventure.apk` (diğer profiller kendi ismini kullanır).
- Profil bir sahne seçer; uygulama sürümünü geriye almaz. versionName/versionCode
  mevcut PlayerSettings değerleridir. Legacy sahne adayı eski imzalı sürümün birebir kopyası değildir.

## Kullanım

Unity menüsü: Tools → Brake Toy → Validate Android Adventure (no APK).
Gerçek APK için aynı menüde Build Android Adventure bulunur.

Komutları proje kökünden çalıştır. Önce Reports/Local klasörünü oluştur.
Unity exe yolunu yerel kurulumuna göre seç; boşluklu yolları tırnakla.

```powershell
& $unityExe -batchmode -nographics -quit -projectPath "$project" `
  -executeMethod BrakeToy.Editor.AndroidPrototypeBuild.Validate `
  -brakeToyProfile Adventure -logFile Reports/Local/BuildPlan.log
```

Validate yalnız planı ve sahnenin varlığını kontrol eder; APK üretmez, model denetimi yapmaz.
Çıktısı `Reports/Local/AndroidBuildPlan.json`; result=ValidatedOnly, buildExecuted=false.
Her çağrı bu geçici plan dosyasını yeniler; eski dosyayı yeni koşunun kanıtı sayma.

Gerçek derleme için Validate yerine Build kullanılır. Profil verilmezse Adventure seçilir.
`-brakeToyProfile Tidy` eski sahneyi açıkça seçer. Çift bayrak, eksik/değersiz bayrak,
bilinmeyen isim, `0` ve `-brakeToyProfile=Adventure` biçimi reddedilir.
Model + APK kontrolü için AdventureValidation.CheckAndBuild hâlâ kullanılabilir;
bu yöntem CLI profil bayrağını değil, açık Adventure profilini kullanır.

## Kanıt ve sınırlar

- 21 yeni Editor testi: 10 profil, eksik sahnede tek sorgu/no fallback, geçersiz enum,
  CLI hata/default/legacy seçimleri, ayar/sahne listesinin korunması ve tekil batch girişleri.
- Toplam EditMode sonucu ve Adventure/Tidy gerçek CLI dry-run kanıtları
  `../Reports/BUILD_PROFILE_VALIDATION.json` içinde kaydedilir.
- Runtime kodu/sahne/prefab değişmedi; önceki 71 PlayMode sonucu yeniden koşulmadı.
- Gerçek BuildPipeline/Android SDK/IL2CPP/APK/imza/kurulum doğrulaması bu turda yapılmadı.
- Dry-run başarısı Android derlemesinin geçtiği veya mağaza paketinin hazır olduğu anlamına gelmez.
- Gerçek build raporu `Reports/ANDROID_BUILD_REPORT-<Profil>-candidate.json` olur;
  tam teslim için ayrıca kaynak commit, APK hash/imza/ABI ve cihaz kanıtı gerekir.
- Masaüstü BrakeToy_Notion_Yedek.txt notları ek yedek olarak güncellenir; Notion tekrar denenmez.
