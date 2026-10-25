# v0.8.1 — Yerleşim sonucu

## 24 Ekim — v0.8.1 yerleşim düzeltmesi

Yeni ToyRoomTidy sahnesi, fix/table-layout-v081 dalı. Mavi parçalar v0.8 oyuncak kasabası kenar dekorlarıydı; 42 m dekor döngüsüyle 64 m defter/kalemlik döngüsü bağımsız olduğundan tekrar tekrar çakışıyordu. Yeni sahnede iki grup aynı 64 m döngüyü ve ayrılmış boşlukları kullanır. Defter/kalemlik en fazla 1.25 birim genişliğe indirildi, ±4.45 kenar merkezlerine oturtuldu; oyun alanına ve masa dışına taşmaları sınandı. Tüm bölge parçaları hücre olarak birlikte akar, pencere/ev parçaları ayrışmaz.

Radyo eski birleşik masa mesh'inden ayrılıp soldaki oyuncak rafının üstüne taşındı; alt yüzeyi gerçek raf üstünden 0.025 birim yukarıda. Sürüş mesafesiyle hareket etmez. Domino model/üretim/ayar düğmesi aktif Tidy sahnesinden kaldırıldı; yerine mevcut rampa fırsatı kullanılır. Önceki v0.8 sahnesi korunur. Hız eğrisi, karşılaşma aralığı, güç süreleri/fiyatları ve kontrol ayarları değiştirilmedi. Kariyer/TP/araç/güç hakları aynı dosyada korunur; geometrisi farklı eski parkurun ghost'u yeni sahnede yüklenmez.

**104/104 Unity testi** (48 EditMode +56 PlayMode) geçti. Üç bölge, dört tam ortak döngü ve yarım metre adımlı yerleşim örnekleri; raf desteği/radyo sabitliği; domino yokluğu ve eski 102 regresyon dahil. Fiziksel cihaz ve insan görüntü/oynanış kabulü bekler. Notion yeni içerik işlemi önceki ücretsiz blok sınırı nedeniyle engelli; otomatik tekrar yapılmadı, yerel/GitHub notları güncel.

## Kontroller

TidyRoomTests, 0–256 m boyunca her 0.5 m'de 42 dekor hücresinin defter/kalemlik XZ alanıyla kesişmediğini, masa dışına veya sürüş şeridine taşmadığını denetler. Sabit ayrılmış yuvalar aynı 64 m fazı kullandığından ileride ayrı döngülerin yeniden üst üste gelmesi kaldırıldı. Dekor anlık gizleme/pat diye kaybolma ile çözülmedi. Radyo üçgenleri kaynak birleşik mesh'ten ayrıldı; önceki mesh dosyaları değiştirilmedi, yeni sahne varyantları üretildi.

9 gerçek Unity renderı: Tidy-0..8.png (menü, garaj, ayarlar, gündüz, gece, 12/42 sn gerçek güvenli sürüş, rampa ve raf/radyo yakın planı). Eski v0.8 kaynakları ve kullanıcı ilerlemesi korunur. Varsayılan yeni sahne Tidy; eski sürümü Tools/Open Playground v0.8 ile incelemek mümkündür.

Bu patch yeni parkur ögeleri üretmez. Sonraki çeşitlilik adayları: farklı zamanlamalı kurmalı oyuncak geçişleri; iki çıkışlı karton tünel/oyun evi; uyarılı sarkaç veya oyuncak vinç; gerçek lokomotif/vagonlu geçiş. Bunlar yalnız öneridir, bu sürümde uygulandı diye sunulmaz. Zorluk kullanıcı isteğiyle korunmuştur.

## Android kanıtı

Non-Development ARM64 IL2CPP paket: 39381857 bayt, SHA256 `edb833836ab8b7a323e5c501fe00bf3c333bb1b81d2918fc14715ef041cdc0c8`. V2 imzası/önceki testlerle aynı sertifika, ZIP bütünlüğü, ZIP16KB ve ELF LOAD16KB doğrulandı. Fiziksel v0.8.1 tablet kabulü bekler.
