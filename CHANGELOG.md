# Değişiklik kaydı

## 0.1.0 — Faz 1 kaynak hazırlığı, 21 Ekim 2026

- Ana gereksinim belgesi eksiksiz korundu; tasarım, mimari ve yedi fazlık yol haritası yazıldı.
- Unity 6.3 LTS, URP 17.3, Input System 1.17, uGUI ve Test Framework manifesti eklendi.
- Dünya ölçeği, aspect'e göre perspektif kamera ve portre ayarları tanımlandı.
- Göreli dokunma/mouse, yatay sınır/hız, bırakma/yeniden dokunma ve görsel yatış eklendi.
- Sabit dizilimli tekrar kullanılan 7 zemin/8 engel; sürekli XZ çarpışma ve hızlı retry kaynakları eklendi.
- Ready/Running/Paused/Crashed ve odak kaybı yönetimi; Türkçe temel UI/safe area eklendi.
- Primitive Sparky, üç engel tipi, zemin, ortak materyal, mobil URP ve bağlantılı sahne üreten korumalı editör aracı yazıldı.
- 19 ortak C# kontrolü geçti; 10.000 seed çarpışma karşılaştırması dahil. Unity EditMode 19/19 ve PlayMode 4/4 geçti; gerçek Input System mouse/touch/UI testleri dahil.
- Unity kuruldu; sahne/prefab/URP assetleri motor içinde üretildi ve Play Mode 3D önizlemesi incelendi.
- Mouse basılı durum geçişinin izlenmesi, sanal giriş testinde bulunan başlangıç olayı sorunu üzerine düzeltildi.
- Unity/Android kurulumu ve doğrulama adımları belgelendi. Fiziksel cihaz ve manuel sürüş hissi kabulü açık bırakıldı.

- Android ARM64 IL2CPP Development APK başarıyla derlendi; imza/arşiv ve paket bilgisi kontrol edildi. Fiziksel cihaz testi yapılmadı.
