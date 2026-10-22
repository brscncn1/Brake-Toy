# v0.6 son doğrulama

39/39 EditMode ve 36/36 PlayMode geçti; 75 toplam. 9/9 Python sunucu testi ve Tests/Network-review.txt gerçek Unity HTTP entegrasyonu geçti. Unity XML saatleri UTC'dir; yerel tarih 23 Ekimdir. Eski regresyonlar dahil.

Yeni kapsam: yedek kayıt/kötü şema/alan doğrulaması, TP idempotence/harcama, Relax kaydı/hakları/120 sn tanık, 10 modelin ortak collider/dört tekerleği, günlük offline/gated online, sıkıştırılmış ghost, gerçek Unity input →Python simülasyonu eşleşmesi. Ayrıntı UNITY_TEST_RESULTS-review.json ve REVIEW_MILESTONE.md.

İlk model çeşitlilik testi yanlış tekerlek mesh'ini seçti; gövde mesh'i seçilerek düzeltildi ve son 36/36 geçti. İlk DX12 yakalama native kapanış hatası sonrası temizlik düzeltildi; D3D11 yakalama 0 ile tamamlandı. Bu olaylar gizlenmedi.

v0.5 tablette kullanıcı kontrol/full grafik öznel akıcılık bildirdi; FPS/p95 ölçülmedi. v0.6 gerçek cihaz, ısı, uzun süre, üretim servis, iOS ve mağaza kabulü yapılmadı.

# v0.5 doğrulama sonuçları

Unity 6000.3.26f1: **31/31 EditMode +31/31 PlayMode =62/62 geçti**. Ayrıntılı test isimleri/saatleri UNITY_TEST_RESULTS.json ve Tests/EditMode-v05.xml / PlayMode-v05.xml içindedir. Önceki raporlar Tests/Archive/v0.4 altında.

- Üç hedefin kümülatif/tek seferlik tamamlanması ve reset; risk yıldızının +75 puan verirken yakın geçiş/kombo uydurmaması.
- 1.000 seed ×128 yeni ritimli satır: deterministik güvenli şerit, bitişik erişilebilir geçişler, tekrar sınırı ve top koridor açıklığı. Eski çeşitlenme/çarpışma matematiği kontrolleri de geçti.
- Dört seed ×güvenli/riskli rota ×120 sn =16 dakika simüle edilmiş v0.5 sürüş: çarpışma yok, engel/yıldız havuzu tükenmedi, instance sayısı büyümedi, riskli rotalar üç hedefi tamamladı. Bu süre gerçek cihazda geçen süre değildir.
- Yıldız çarpışma alt adımında ödül vermiyor, bir kez toplanıyor, pause ilerlemiyor, retry sıfırlıyor. Kayıtlar testlerde yazılmaz.
- Kamera 9:16 /9:19.5 /9:21 /3:4 oranlarında ve iki direksiyon ucunda araç ile 27 birimlik tepki bölgesini kadrajda tutuyor.
- Yeni ses referansları, iki ayrı ses ayarı, menü görünürlüğü, özel kalite pipeline kopyası ve postprocessing geçişi doğrulandı.
- Araç sonlu vertex/normal ve collider sınırları içinde. Scene mesh/material/index/shader/dekor collider denetimi: MODEL_VALIDATION.json. Sayımlar inaktif engel havuzunu da içerir.
- Önceki sahnelerin kontrol/döngü/giriş/kamera/model/kombo testleri geçti.
- Android ARM64 IL2CPP Development paket derlemesi, ZIP/native library, aapt2 sürüm/API/paket ve apksigner v2 doğrulaması geçti; APK özeti ANDROID_BUILD_REPORT.json.

**Açık kabul:** Fiziksel Huawei MatePad 11 FPS/p95/ısı/dokunma/arka plan ve insan ses/tempo/sanat değerlendirmesi yapılmadı. Otomatik input testi gerçek dokunma gecikmesini ölçmez. Yüksek 60 /ekonomik 30 FPS birer hedeftir. v0.5 final sürüm veya onaylanan konseptin birebir gerçekleşmesi değildir.
