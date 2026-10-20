# v0.2 — Doğrulama raporu

Unity 6000.3.26f1 ile yeni ToyRoomTabletop sahnesi ve tüm C# assembly'leri derlendi.

## Çalıştırılan testler

- **EditMode: 19/19 geçti.** Aynı üretim SteeringModel/SweptCollision kontrolleri; 10.000 seed segment/kutu oracle karşılaştırması dahil.
- **PlayMode: 8/8 geçti.** İlk 4 test orijinal sahneye regresyon; yeni 4 test oda sahnesi ve dekor akışı içindir.
- Oda dekorlarının collider'ı yok; araç/engel gameplay ölçüleri korunuyor; tekerlek GameManager bağlantısı kalıcı.
- Yeni sahnede çarpışma, tekrar başlama, zemin/engel kimliklerinin yeniden kullanımı doğrulandı.
- Kamera 9:16, 9:19.5, 9:21 ve 3:4 oranlarında araç alt %25 ve track kenarlarını koruyor. 1.5 saniyelik ileri mesafe kadrajda; bu bir insan refleksi veya prosedürel parkur adalet garantisi değildir.
- Yakın masa dekorları WorldScroller'ın gerçek ilerleme miktarıyla hareket eder, pause sırasında durur ve restart'ta başlangıca döner.
- Kanıt: `Tests/EditMode-v0.2-results.xml`, `Tests/PlayMode-v0.2-results.xml`, `UNITY_TEST_RESULTS.json`.

## Görsel kontrol

Gerçek grafik açık Unity Play Mode'da 20° ve 28° renderlar alınıp incelendi. Oda görünürlüğü nedeniyle 20° seçildi. Büyük düz yüzeylerde bevel normal interpolasyonunun yarattığı bantlar düzeltilip ahşap dokusu sadeleştirildi. Son görüntü `Preview-v0.2.png`; imagegen konseptiyle karıştırılmamalı. Bu bir ara sanat sürümüdür.

## Android

ARM64 / IL2CPP Development APK üretildi: yaklaşık 37.0 MiB. APK imzası, ZIP bütünlüğü, libil2cpp.so, paket adı, sürüm ve SDK/ABI bilgileri doğrulandı. `ANDROID_BUILD_REPORT.json` hash ve boyutu içerir.

## Henüz yapılmayan

Fiziksel Android dokunma/safe area/kesinti ve uzun sürüş testleri; gerçek cihaz CPU/GPU frame time, bellek/ısı; iOS; kullanıcı tarafından yeni kamera ve kontrol hissinin onayı. Mağaza yayını, nihai art, prosedürel üretici ve online uygunluk tamamlanmadı.

Önceki Faz 1 raporları `Docs/Archive/v0.1` içinde korunur. Çalıştırılmayan testler geçmiş gibi işaretlenmez.
