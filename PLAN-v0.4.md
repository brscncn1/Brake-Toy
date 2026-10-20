# v0.4 — Refined Room uygulama planı

Amaç: Faz 2 üretim altyapısındaki açıkları kapatırken Faz 3'ün tek oda kalite çalışmasını ilerletmek. Referans eleştiri: QUALITY_REVIEW-v0.3.md. Bu plan tamamlanma raporu değildir.

1. Yeni ToyRoomRefined sahnesi, RefinedConfig ve ayrı sanat varlıkları. v0.3 korunacak.
2. Kamera yan boşluğu/pitch ayarı, sakin ahşap yüzeyi ve yumuşak gölge; araç kabin/cam oranı. Aynı koşullarda gerçek motor karşılaştırması.
3. Tek menü paneli, küçük HUD, sürüşte ikincil düğmeleri gizleme.
4. ObstacleDefinition kataloğu; deterministik ama çeşitlenen kalıp/aralık/top yolları. Yeni tur ve aynı parkuru tekrar etme ayrımı.
5. Düşük/yüksek grafik profilleri; değişiklikler çalışma anı kopyasında uygulanacak. Kullanıcının rekorları korunacak.
6. Katmanlı motor/teker ve malzemeye göre oyuncak çarpışma sesi; kısa grounded görsel tepki ve sonuç panelinin zamanlaması.
7. Yerel performans ölçümü, gerçek cihaz matrisi ve uygulanabilir tablet denemesi.
8. Eski regresyonlarla birlikte yeni kalıp/katalog/UI/profil/mesh testleri; Android APK, rapor, GitHub ve Notion teslimi.

Kapsam dışı: yeni harita, garaj/ekonomi/online. Dokuz kalite önerisinin uygulaması bu sürümdedir; profesyonel kayıt paketi veya final sanat düzeyine ulaşıldığı iddia edilmez. Fiziksel cihaz kabulü gerçek ölçüm ve kullanıcı denemesi gerektirir.

## Kullanıcının kapsam kararı — tüm öneriler bu sürümde

Kalite raporundaki dokuz başlık aynı v0.4 çalışmasına alındı. Ses ve çarpışma cilası sonraya bırakılmayacak: katmanlı motor/teker sesi, kuru oyuncak çarpışması ve kısa görsel tepki. Oda ayrıntıları ve gerçek motor önce/sonra karşılaştırması da teslimde yer alacak. Fiziksel cihaz FPS/ısı/dokunma kabulü için cihaz matrisi ve ölçüm kaydı hazırlanacak; cihaz olmadan ölçüldü denmeyecek. Önceki dar kapsam ifadeleri bu kararla güncellendi. Yeni harita/garaj/online bu kalite raporunun önerileri arasında değildir.
