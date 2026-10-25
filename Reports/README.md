# Raporlar ve görsel kanıtlar

- Bu klasörde seçilmiş sürüm/test/build JSON kanıtları bulunur.
- `Tests/`: geçmiş Unity XML sonuçları ve yerel ağ kontrol çıktısı.
- `Visuals/`: motor renderları, karşılaştırmalar ve HTML galerileri.
- `Local/`: geçici koşular; `.gitignore` ile dışarıda tutulur.
- `ORGANIZATION_MANIFEST.json`: 25 Ekim taşıma haritası ve özgün SHA256 değerleri.
- Eski kilometre taşı/plan belgeleri ve teslim JSON'ları `../Docs/Archive/pre-v09/` içindedir.

Editor model denetimleri ve render araçları `ReportPaths` üzerinden buraya yazar.
Harici build/test komutlarında çıktı yolunu açıkça bu klasöre yönlendir:

```powershell
dotnet run --project Tests/Standalone -- Reports/Local/CoreChecks.json
# Unity Test Runner: -testResults Reports/Local/EditMode.xml
# Unity günlükleri: -logFile Reports/Local/Unity.log
```

Önce `Reports/Local` klasörünü oluştur. Unity test komutunun tamamı `../DOGRULAMA.md` içindedir.
Yerel çıktıyı ancak inceleyip sürüm kanıtı olarak seçtikten sonra kalıcı raporlara taşı.
Galeri HTML dosyalarını aynı klasördeki PNG dosyalarıyla birlikte aç.
JSON/XML/PNG kanıtları taşınırken içerikleri değiştirilmedi; eski JSON içindeki yollar
özgün koşunun konumlarını anlatır. Eski commit bağlantıları da o commit için geçerlidir.
Bu düzenleme yeni oyun testi, APK veya cihaz kabulü anlamına gelmez.
