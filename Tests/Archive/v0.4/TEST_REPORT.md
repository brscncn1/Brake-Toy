# v0.4 doğrulama raporu

Unity 6000.3.26f1: **53/53 geçti** — 28 EditMode, 25 PlayMode. XML sonuçları Tests klasöründe; v0.3 raporları Tests/Archive/v0.3 altında korunur.

- Eski 19 çekirdek kontrolü ve v0.1/v0.2/v0.3 oynanış regresyonları geçti.
- Yeni çeşitlenen üreticide 1000 seed ×128 =128.000 satır güvenli koridor, en fazla komşu şerit geçişi, en fazla iki aynı güvenli koridor, değişken boşluklar ve 5–8 satırlık top olayları sınandı. Aynı seed aynı mesafe/koridor/eğimi üretti. Eski 128.000 satır kontrolü de geçti.
- 10.000 göreli hareketli top-kutu çarpışması ince örnekleme ve temas geometrisiyle karşılaştırıldı. Hız integrali 30/60/120 FPS ile uyuştu; ilk düz sürüş çarpışması 1.5–2.5 sn içinde.
- Hem v0.3 hem v0.4 iki dakikalık motor içi tanık sürüşünde havuz tükenmeden ve yeni tehlike nesnesi oluşturmadan ilerledi. Bu insan oynanış/eğlence testi değildir.
- Yeni tur/aynı parkur, rekor yazmayan testler, katalog ölçüleri, tek panel/kompakt HUD, sürüşte ikincil düğmelerin kapanması ve grafik kaynağının çalışma anı kopyasıyla izole edilmesi doğrulandı.
- Altı ses türünün örnekleri sonlu, tepe değeri .91 altında, baş/son sıfır ve RMS sınırında. Bu tablet hoparlörü veya insan dinleme kabulü değildir.
- Çarpışma animasyonu gameplay kökünü/collider'ı kaydırmadı; kısa tepki bittiğinde kamera/visual geri geldi ve sonuç paneli açıldı.
- 9:16, 9:19.5, 9:21 ve 3:4 portrede maksimum hızdaki 1.5 sn tepki alanı görünür. Araç gameplay kutusunda; top gerçek vertexleri dönerken zeminin altında değil. Mesh/indis/malzeme ve dekor collider denetimleri geçti.
- Ön/arka/engel ve oynanış gerçek renderları incelendi. Son düzeltmede masa parçaları ve pencere çevresindeki duvar panel çizgileri kesintisiz yüzeyle giderildi; sonra PlayMode yeniden çalıştırıldı ve APK yenilendi.
- Model denetimi: 61 benzersiz mesh, 192 renderer, tüm inaktif havuz dahil 158146 üçgen; 0 eksik mesh/malzeme, 0 shader hatası, 0 dekor collider. Bu sayılar gerçek draw call/FPS değildir.
- 6.3 sn ve 24 sn gerçek risk sürüşü renderlandı; geç sürüş 325.9549 m, 3000 stil, zincir 10. Kazanılmamış skor enjekte edilmedi.
- Android ARM64 IL2CPP Development APK başarıyla derlendi; ZIP/native kitaplık, v2 imzası, paket/sürüm/API denetimleri geçti. İlk build'de eksik Android JNI yerleşik modülü yakalanıp eklendi. Son APK yüzey düzeltmelerini içerir.

Fiziksel Huawei MatePad 11 performansı/ısı/dokunma ve gerçek kullanıcı tempo/ses/sanat kabulü bekliyor. Profil hedefleri 60/30 FPS'dir; ölçülmüş sonuç değildir. Oyun içi 60 sn yerel ölçüm ve DEVICE_TEST-v0.4 cihaz matrisi hazır. iOS test edilmedi. Onaylı konsept hâlâ mevcut oyundan daha zengin bir sanat hedefidir.
