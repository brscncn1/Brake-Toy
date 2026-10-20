# v0.2 — Bedroom Tabletop teslimi

## Değişen deneyim

Önceki düz zemin prototipi yerine çocuk odasının ahşap oyun masasında sürüş. Gerçek 3D pencere/perde, raf/oyuncak ayı, yatak, poster, sandalye, oda zemini ve masa yapısı eklendi. Yakın kalemlik, defter ve çıkartmalar zeminle birlikte kayar; uzak oda sabit görsel referans sağlar. Dekorlar çarpışma kuralı oluşturmaz.

Sparky ve kitap/blok/silgi modelleri yuvarlatıldı; plastik ve ahşap materyalleri, tekerlek animasyonu ve daha hafif menü örtüsü eklendi. 20° kamera odayı gösterirken araç alt %25'te kalır. Masa genişliği 12 birim; gameplay alanı ve kontrol/collider değerleri korundu.

## Dosyalar

- `Assets/BrakeToy/Scenes/ToyRoomTabletop.unity` ve `ScriptableObjects/RoomConfig.asset`.
- `Art/Tabletop`, `Prefabs/Tabletop`, `Editor/TabletopSceneBuilder.cs`.
- `ToyWheelAnimation.cs`, `TabletopDecorScroller.cs`; WorldScroller'ın görsel ilerleme/reset olayları.
- `Preview-v0.2.png` ve 28° karşılaştırması. Orijinal ToyRoom/Prefab/Preview ve v0.1 APK korundu.
- `Builds/Android/BrakeToy-v0.2-Tabletop.apk`: yaklaşık 37.0 MiB.

## Doğrulama

Unity **27/27 test geçti**: 19 EditMode, 8 PlayMode. Android APK imza/arşiv/paket kontrolü geçti. Fiziksel cihaz testi yapılmadı; FPS ve kontrol hissi henüz onaylanmadı.

## Kullanım ve sonraki adım

Unity Tools → Brake Toy → Open Bedroom Tabletop v0.2; Game oranı 9:16; Play → OYNA. Sonra APK'yı gerçek telefonda kontrol et. Bu sahne onaylanan konsept yönünde bir ara sürümdür; nihai görsel kalite ve oda geçişleri hâlâ geliştirilecek. Sırada, kullanıcı geri bildirimi sonrası Faz 2 adil engel üretimi, hızlanma, mesafe ve Near Miss/kombo vardır.
