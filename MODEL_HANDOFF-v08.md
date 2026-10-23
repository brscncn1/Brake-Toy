# v0.9 ek model devir notu

Güncel sahne ToyRoomAdventure. Sekiz yeni maket Assets/BrakeToy/Art/Adventure/Prefabs altında; ayrıntılı görsel model çalışması kullanıcı tarafından sonraya bırakılmıştır. AdventureToy.Moving/Options/ExitArrows/Link/Shadow pivotlarını ve AdventureSolid hacimlerini koru. Gerçek model ölçüsü değişirse çizimle analitik çarpışma hacmini birlikte güncelle; yeni PlayMode senaryolarını çalıştır. Tünel tavanı uçuş/jetpack için yüksek; cetvel her iki kenardan desteklenir, vinç yükü sabit uzunluklu askıyla hareket eder. Modeller bütün şeritleri kapatamaz; açık yeşil yol ve raf/masa kenarı yerleşim boşlukları korunmalı. Kaynak ve test ölçütleri MILESTONE-v09.md / PLAN-v0.9.md. Önceki v0.8 devir notu aşağıda tarihsel olarak korunur.

# Sonraki model çalışması için bağlantı sözleşmesi

Model ayrıntısı kullanıcı tarafından sonraki agent çalışmasına ayrıldı; bu oturumda başka agent çalıştırılmadı. v0.8 dokuz ayrı temel siluet ve korunmuş Sparky içerir. Bu şekiller üretim sanat kabulü değildir.

- Sahne: Assets/BrakeToy/Scenes/ToyRoomPlayground.unity.
- Yeni araç varlıkları: Assets/BrakeToy/Art/Playground/Vehicles/Car_1..9.asset ve .prefab. Sparky eski referansını kullanır. VehicleDefinition.VisualPrefab garajı, ReviewSession.Visuals sahne oyuncusunu besler; ikisi birlikte değişmeli.
- Kök merkez X/Z=0; taban Y≈0. Araç ileri +Z. Dört doğrudan Wheel adlı çocuk ToyWheelAnimation tarafından sürülür. Alt objelerde Collider bulunmamalı; gameplay collider GameConfig'ten gelir ve bütün araçlarda eşittir.
- Görsel ana gövde yaklaşık ±0.4 X / ±0.65 Z içinde; tekerlekler korunmuş Sparky oranında. Merkez ekseni ve teker pivotu korunmalı. PowerRig seçili Visual konum/rotasyonunu izler, gameplay collider değildir.
- Yeni modelde materyal/mesh referansları prefab ve sahnede kalıcı olmalı. Eski sahne/prefabların üzerine yazılmamalı. Yakın plan ön/arka ve gerçek portre kamera; taban/gövde-teker, far/cam iç içe geçme ve üçgen/materyal maliyeti kontrol edilmeli.
- İsimleri farklı boyalar olarak sunmak yeterli değil: Bubble Bug yuvarlak kısa, Mini Muscle uzun kaputlu, Candy Cruiser açık, Pixel Kart açık teker, Rocket Pop ikiz pod, Thunder Volt alçak, Galaxy Glider kubbeli, Golden Comet roadster, Crown Racer yüksek burunlu prestij.
