27 Ekim Android teslimi: v0.10.0/kod11 başarıyla derlendi, ARM64/IL2CPP ve test imzası doğrulandı.
GitHub Release https://github.com/brscncn1/Brake-Toy/releases/tag/v0.10.0 ; PR https://github.com/brscncn1/Brake-Toy/pull/21 .
Kaynak2f2d3f41ad530d90516d55a5cf61c28bc5018139; APK SHA256 3b0a3c8c57df60cec57a0c7e1d95ef17484b8f98dccd8f9ead53f52da3f88bb5 .
GitHub digest yerelle aynı.187benzersiz Unity testi; fiziksel tablet ve kullanıcı sanat kabulü bekliyor. K6 başlamadı.

# Oyuncu deneyimi paketleri â€” 27 Ekim 2026

R1 HUD/menÃ¼/ayar/sonuÃ§; R2 son3deseni hatÄ±rlayan seed parkuru ve3ritim/Ã¶dÃ¼l rolÃ¼;
R3 Ã¼Ã§ yeni gÃ¶vde ve geri alÄ±nabilir saten masa Ã¶rneÄŸi; R4 Adventure benchmark/kayÄ±t izolasyonu
ve tablet kabul fÃ¶yÃ¼ uygulandÄ±.187benzersiz Unity testi, son3kabul testi tekrar3/3 geÃ§ti.
GerÃ§ek kariyer/ghost dosyalarÄ± aynÄ±. Sanat beÄŸenisi ve gerÃ§ek cihaz kabulÃ¼ bekliyor.
Tablet baÄŸlanamÄ±yor; Ã¶lÃ§Ã¼lmemiÅŸ FPS veya dokunma kabulÃ¼ iddiasÄ± yok. K6 baÅŸlamadÄ±.
AyrÄ±ntÄ±lar Docs/PLAYER_EXPERIENCE_PACKAGES-2026-10-27.md. Android derleme sonucu ayrÄ±ca kaydedilecek.
Notion kotasÄ± tekrar dÃ¶ngÃ¼yle denenmedi; kullanÄ±cÄ± isteÄŸiyle masaÃ¼stÃ¼ yedeÄŸi tutuldu.

# Oda aydÄ±nlÄ±ÄŸÄ±, rampa seÃ§imi ve oyuncu incelemesi â€” 26 Ekim 2026

KullanÄ±cÄ± durdurduktan sonra 'devam et' dedi; kayÄ±tlÄ± checkpoint'ten sÃ¼rdÃ¼rÃ¼ldÃ¼. Ä°ki somut hata: RoomDayCycle ve PlaygroundAtmosphere birlikte iÃ§ mekÃ¢nÄ± gece karartÄ±yordu; ilk geÃ§it boÅŸluÄŸu daima rampa dÄ±ÅŸÄ±ndaki giriÅŸ hattÄ± olduÄŸundan her rampanÄ±n Ã§Ä±kÄ±ÅŸÄ± engel anlamÄ±na geliyordu. Adventure iÃ§ ana Ä±ÅŸÄ±ÄŸÄ±nÄ±n ÅŸiddeti/rengi/yÃ¶nÃ¼ ve ortam Ä±ÅŸÄ±ÄŸÄ± gÃ¼ndÃ¼z deÄŸerinde sabitlendi; dÄ±ÅŸ gÃ¶kyÃ¼zÃ¼/ay/yÄ±ldÄ±z ve iki lambanÄ±n dÄ±ÅŸ geceye gÃ¶re aÃ§Ä±lmasÄ± korundu. Rampa ilk Ã§Ä±kÄ±ÅŸÄ± seed'e gÃ¶re aÃ§Ä±k/alÃ§ak engelli; aÃ§Ä±k Ã§Ä±kÄ±ÅŸtaki nesne Ã¼Ã§Ã¼ncÃ¼ ÅŸeritte5birim Ã¶nceye taÅŸÄ±nÄ±yor, silinmiyor.6hazard/cÃ¼mle,10katalog tÃ¼rÃ¼, masa eÅŸyalarÄ±, otobÃ¼s, top profilleri ve rampalar korunuyor. Ghost tabletop-r5-choices ad alanÄ±na ayrÄ±ldÄ±; eski dosyalar silinmedi.

91EditMode+GPU88PlayMode=179/179.100seedÃ—120cÃ¼mle daÄŸÄ±lÄ±mÄ±;7rampa ailesiÃ—2Ã§Ä±kÄ±ÅŸta14dizilim; kurmalÄ± oyuncakta aÃ§Ä±k/engelliÃ—rampa/bypassÃ—30/60/120Hz12gÃ¼Ã§sÃ¼z rota; Ã¶nceki18kaÃ§Ä±ÅŸ tanÄ±ÄŸÄ± geÃ§ti. GerÃ§ek kariyer/ghost hash'leri bu devam oturumunda deÄŸiÅŸmedi.12Ã¼rÃ¼n akÄ±ÅŸÄ± ve4oda/rampa karesi alÄ±ndÄ±. Yeni APK/tablet FPS/p95 veya insan kullanÄ±labilirlik kabulÃ¼ yok.

Docs/FRESH_EYES_REVIEW-2026-10-26.md yeni oyuncu akÄ±ÅŸÄ± Ã¼zerinden olumsuz bulgular ve kabul Ã¶lÃ§Ã¼tlÃ¼ Ã§Ã¶zÃ¼m planÄ±dÄ±r. BaÅŸlÄ±ca sorunlar: altÄ± sabit dÃ¼zen omurgasÄ±; rampanÄ±n fazla tek tip rolÃ¼; HUD kontrastÄ± ve uyarÄ± konumu; ayarlarda alt alan/geri gÃ¶rÃ¼nÃ¼rlÃ¼ÄŸÃ¼ (gÃ¶rsel bulgu, raycast henÃ¼z Ã¶lÃ§Ã¼lmedi); sÄ±fÄ±r uÃ§uÅŸta bile hava bonusu yazÄ±sÄ± ve eski PLAYGROUND adÄ±; incelenen3garaj aracÄ±nda ortak gÃ¶vde; malzeme/model kalitesi; tur/kariyer hedef ayrÄ±mÄ±; ilk menÃ¼ seÃ§enek yoÄŸunluÄŸu. DiÄŸer Ã§Ã¶zÃ¼mler bu pakette uygulanmadÄ±. Ã–nerilen sonraki tek paket HUD/menÃ¼/sonuÃ§ saÄŸlamlaÅŸtÄ±rmasÄ±; K2 devam ediyor, K6 baÅŸlamadÄ±. Kaynak dalÄ± fix/bright-room-ramp-choices, kanÄ±t Reports/ROOM_RAMP_REVIEW_VALIDATION.json. Notion kotasÄ± tekrar denenmedi; yerel gÃ¼nlÃ¼k ve masaÃ¼stÃ¼ yedeÄŸi gÃ¼ncellendi.

# Top yuvarlanmasÄ± ve iki gece lambasÄ± â€” 26 Ekim 2026

BÃ¼yÃ¼k top sabit engel gibi Ã¼retildiÄŸi ve masa kaymasÄ± dÃ¶nÃ¼ÅŸe katÄ±ldÄ±ÄŸÄ± iÃ§in yerinde dÃ¶nÃ¼yordu. MotionKind3 baÄŸÄ±msÄ±z ileri hareket, dÃ¼z/Ã§apraz/tek kavis/Ã§ift kavis profilleri ve ilerleyen parkurda sÄ±nÄ±rlÄ± hÄ±z artÄ±ÅŸÄ± eklendi. Yuvarlanma gerÃ§ek masa Ã¼stÃ¼ yer deÄŸiÅŸtirme/radyus; sabit top gereksiz dÃ¶nmez. KÃ¼Ã§Ã¼k misketlerin1/2/3lÃ¼ grubunda farklÄ± profiller vardÄ±r. BaÅŸlangÄ±Ã§ derinliÄŸi hÄ±zla Ã¶lÃ§eklenir; PositionAfter ile gerÃ§ek adÄ±m aynÄ± olduÄŸundan swept Ã§arpÄ±ÅŸma korunur. Top uyarÄ±sÄ± mevcut sol/orta/saÄŸ konumu sÃ¶yler. Kitap/defter/kalemlik, otobÃ¼s, diÄŸer oyuncaklar ve rampalar azaltÄ±lmadÄ±. Yeni hareket eski ghost'a yÃ¼klenmez; dosyalar korunur.

Gece radyonun yanÄ±ndaki raf ve yatak baÅŸlÄ±ÄŸÄ±na iki loÅŸ lamba. GÃ¼ndÃ¼z Ä±ÅŸÄ±klarÄ± sÃ¶ner; hafif parkur spotu TrackSpill=false ile kapanabilir. AyaklÄ± deneme dekoru delme riski nedeniyle bÄ±rakÄ±ldÄ±, yatak baÅŸlÄ±ÄŸÄ±na oturan sÄ±ÄŸ taban kullanÄ±ldÄ±. Ä°ki yeni Ä±ÅŸÄ±k gÃ¶lgesiz; model ve materyaller baÅŸlangÄ±Ã§ta bir kez Ã¼retilir, sahne kapanÄ±nca temizlenir.

Son kaynakta90EditMode+GPU84PlayMode=174/174. DÃ¶rt yÃ¶rÃ¼nge,120varyant, masa Ã¼stÃ¼ gerÃ§ek hareket ve yol/radyus dÃ¶nÃ¼ÅŸÃ¼,30/60/120Hz adÄ±m tutarlÄ±lÄ±ÄŸÄ±, top Ã§arpÄ±ÅŸmasÄ±, gÃ¼n/gece/spot kontrolleri ve Ã¶nceki18gÃ¼Ã§sÃ¼z parkur tanÄ±ÄŸÄ± geÃ§ti. BeÅŸ gerÃ§ek Unity karesi kaydedildi. GerÃ§ek kariyer/ghost hash'leri deÄŸiÅŸmedi. Ä°nsan hareket/gece kabulÃ¼ ve tablet FPS/p95 Ã¶lÃ§Ã¼mÃ¼ aÃ§Ä±k; yeni APK Ã¼retilmedi. AyrÄ±ntÄ± Docs/ROLLING_AND_NIGHT-2026-10-26.md, kanÄ±t Reports/ROLLING_NIGHT_VALIDATION.json. Dal fix/rolling-ball-motion. K2 devam ediyor, son K6 aÅŸamasÄ±na geÃ§ilmedi. Notion kotasÄ± otomatik tekrar denenmedi; bu not masaÃ¼stÃ¼ yedeÄŸinde de tutuldu.

# K2.4 parkur hatasÄ±nÄ±n dÃ¼zeltilmesi â€” 26 Ekim 2026

KullanÄ±cÄ± mevcut nesneleri azaltmayÄ± onaylamamÄ±ÅŸtÄ±. Adventure, eski akÄ±ÅŸÄ±n yerine geÃ§ince otobÃ¼sÃ¼ hiÃ§ Ã§aÄŸÄ±rmÄ±yor, Joyful kataloÄŸundaki oyuncaklardan yalnÄ±z birini ekliyordu; kitap/spiral defter/kalemlik de parkurda yoktu. ÃœÃ§ ayrÄ± masa eÅŸyasÄ± prefabÄ± geri eklendi; sekiz ana oyuncak ailesi, ilk yedi hazard ve kitap rampalarÄ± korundu. AltÄ± dÃ¼zen, Ã¼Ã§ Ã§ift engelli geÃ§it, seed ile Â±2 mesafe ve slalomda1.5 ÅŸaÅŸÄ±rtma eklendi. OtobÃ¼s altÄ± dÃ¼zenin ikisinde Â±0.65 yatay hareket eder. SÃ¼rekli yeÅŸil bypass yok; geÃ§it boÅŸluÄŸu bÃ¶lÃ¼m iÃ§inde deÄŸiÅŸir, tÃ¼nelin kÄ±sa Ã§Ä±kÄ±ÅŸ iÅŸareti ve rampa oku kalÄ±r. DenseCourse tur baÅŸÄ±nda sabitlenir; ghost tabletop-r3 ad alanÄ±na ayrÄ±lÄ±r, eski dosyalar korunur. Eski profil karÅŸÄ±laÅŸtÄ±rma iÃ§in saklanÄ±r.

85 EditMode +GPU aÃ§Ä±k80 PlayMode =165/165. Yeni modda18adet60sn gÃ¼Ã§sÃ¼z tanÄ±k:2mod/3seed/30-60-120Hz; can kaybÄ± ve havuz tÃ¼kenmesi yok. Sabit Ã¼Ã§ ÅŸeritte beklemek turu geÃ§irmiyor. Model hacimleri/fiili Ã¼retim ve otobÃ¼sÃ¼n tÃ¼m salÄ±nÄ±mÄ±nda boÅŸluk kontrol edildi.100seed/mod Ã—120cÃ¼mle deterministik kural kontrolÃ¼;6gerÃ§ek motor karesi. GerÃ§ek kariyer/ghost hash'leri deÄŸiÅŸmedi.181taÅŸÄ±ma/150deÄŸiÅŸmez kanÄ±t/146eski link dÃ¼zen kontrolÃ¼ geÃ§ti. Bunlar tablet FPS veya insan oynanÄ±ÅŸ kabulÃ¼ deÄŸildir. Yeni APK yok. Dal feat/dense-tabletop-course; kanÄ±t Reports/TABLETOP_COURSE_VALIDATION.json; ayrÄ±ntÄ± Docs/TABLETOP_COURSE-2026-10-26.md. K2 revizyonundayÄ±z; K6 son aÅŸamaya geÃ§ilmedi. Notion kotasÄ± tekrar denenmedi; bu not masaÃ¼stÃ¼ yedeÄŸine de eklendi.

# K2.3/K2.4 bitiriÅŸ deneyi â€” 26 Ekim 2026

K2.3/K2.4 iki ailelik deney tamam: kurmalÄ± oyuncak ve tren, seed varyantÄ±na gÃ¶re yerden Ã¼Ã§ yÄ±ldÄ±z rotasÄ± veya kitap rampasÄ±/hava Ã¶dÃ¼lÃ¼yle biter. Her iki bitiriÅŸte Ã¼Ã§ yÄ±ldÄ±z vardÄ±r; hava TP bonusu ve eriÅŸilebilirlik aynÄ± deÄŸildir. HÄ±z, ana aile sÄ±rasÄ±, karÅŸÄ±laÅŸma mesafesi, kariyer ve fiyatlar korunur. Yeni altÄ±n ok0.079yÃ¼ksekliÄŸinde yÃ¼zeyin Ã¼zerinde gÃ¶rÃ¼nÃ¼r. VariedEndings=false ve yeni turla klasik dizilim geri gelir; aktif profil tur baÅŸÄ±nda sabitlenir. adventure-r2-varied/classic ghost ad alanlarÄ± ayrÄ±; eski ghost dosyalarÄ± silinmez. 82EditMode+77PlayMode=159/159; son katalog eÅŸlemeli gÃ¶rsel fixture kontrolÃ¼5/5 ayrÄ±ca geÃ§ti. AynÄ±3seed/2mod/2profilde12adet60sn witness: aynÄ± mesafe/Ã¼retilen cÃ¼mle, kaybedilmemiÅŸ haklar, havuz sÄ±nÄ±rlarÄ±. Ä°ki aile/iki bitiriÅŸ/30-60-120Hz12risk rotasÄ± gÃ¼Ã§sÃ¼z geÃ§ildi;16dizilimde Ã¶dÃ¼l bÃ¼tÃ§esi kontrol edildi;4motor karesi kaydedildi. Yeni APK/tablet/insan kabulÃ¼ yok. GerÃ§ek kariyer/ghost dosyalarÄ± deÄŸiÅŸmedi. KanÄ±t Reports/ENDING_VARIANTS_VALIDATION.json, ayrÄ±ntÄ± Docs/ENDING_VARIANTS-2026-10-26.md. Sonraki paket K2.5 uyarÄ± okunabilirliÄŸidir. Son planlÄ± aÅŸama henÃ¼z baÅŸlamadÄ±; K6baÅŸÄ±nda haber verilecek. Notion tekrar denenmedi; masaÃ¼stÃ¼ yedeÄŸi gÃ¼ncellendi.

# K2.1 ilk dakika â€” 25 Ekim 2026

K2.1 Ã¶lÃ§Ã¼mÃ¼ tamamlandÄ±: 100 seed/mod, 200 parkur/600 pencere. Arcade10/30/60 sn:4/11/22 cÃ¼mle referansÄ±; Relax:2/7/15. GÃ¼venli yol geÃ§iÅŸlerinin yÃ¼zde100Ã¼ merkez-kenar. Runtime gÃ¼venli witness 3 seed/2 mod/60sn ile18 ara Ã¶rnek verdi; analitik sayÄ±larla eÅŸleÅŸti, hak kaybÄ±/pool tÃ¼kenmesi yok. 7331 Arcade10/30/60 sn iÃ§in3 motor gÃ¶rÃ¼ntÃ¼sÃ¼ kaydedildi (video deÄŸil). 77EditMode+72PlayMode=149/149. PNG dÄ±ÅŸa aktarmasÄ± iÃ§in Unity yerleÅŸik imageconversion1.0.0 aÃ§Ä±k baÄŸÄ±mlÄ±lÄ±k yapÄ±ldÄ±; ilk derlemede eksikliÄŸi dÃ¼zeltilmiÅŸ oldu. HÄ±z/Ã¼retim/Ã¶dÃ¼l/model kurallarÄ± deÄŸiÅŸmedi. Merkez-kenar ve ortak bitiriÅŸlerin tekrar hissine katkÄ±sÄ± bir Ã§Ä±karÄ±mdÄ±r; insan karar/uyarÄ±/tutunma Ã¶lÃ§Ã¼mÃ¼ yok. 30birim cÃ¼mle bÃ¼tÃ§esinden Ã§Ä±kan ara ekranda boÅŸluk sÃ¼resi deÄŸildir. Sonraki tek deney hÄ±z sabitken iki ailede yerden Ã¶dÃ¼l/kitap rampasÄ± bitiriÅŸlerini Ã§eÅŸitlendirmek. APK/tablet/video yok; gerÃ§ek kariyer/ghost dosyalarÄ± deÄŸiÅŸmedi. PlanÄ±n son aÅŸamasÄ±na henÃ¼z gelinmedi; K6ya baÅŸlarken kullanÄ±cÄ±ya aÃ§Ä±kÃ§a haber verilecek. Notion tekrar denenmedi; masaÃ¼stÃ¼ yedeÄŸi gÃ¼ncellendi. AyrÄ±ntÄ± Docs/OPENING_PACE-2026-10-25.md, kanÄ±t Reports/OPENING_VALIDATION.json.

# K1.2 temiz klon â€” 25 Ekim 2026

K1.2 temiz klon kontrolÃ¼ tamamlandÄ±. GitHub df4ba16189dc482b8800f839b51e95cac8c74f76 yeni klasÃ¶re alÄ±ndÄ±; baÅŸlangÄ±Ã§ta git temizdi ve Library yoktu. Yerel proje dosyalarÄ±/Library kopyalanmadÄ±; aynÄ± makinenin global Unity paket Ã¶nbelleÄŸi kullanÄ±labilir. Unity 6000.3.26f1 taze import ve derlemesi sonrasÄ± 74/74 EditMode, GPU aÃ§Ä±k 71/71 PlayMode =145/145 geÃ§ti. Ä°lk Null Graphics Device koÅŸusu 70/71 idi: garaj/UI RenderTexture hatasÄ±. Oyun kodu deÄŸiÅŸmeden D3D11 ile 71/71 geÃ§ti; test rehberindeki PlayMode komutu buna gÃ¶re dÃ¼zeltildi. BaÄŸÄ±msÄ±z .NET kontrolleri 19/19 (SDK10.0.402/runtime8.0.21), referans sunucu 9/9 (Python3.14.0). Adventure CLI planÄ± exit0/ValidatedOnly, APK Ã¼retilmedi. ÃœÃ§ otomatik biÃ§im farkÄ± semantik eÅŸitlik doÄŸrulanarak geri alÄ±ndÄ±; klon kaynaÄŸÄ± temiz ve git fsck baÅŸarÄ±lÄ±. GerÃ§ek kariyer/ghost dosyalarÄ± deÄŸiÅŸmedi. Yeni oyun Ã¶zelliÄŸi, APK, tablet veya sanat kabulÃ¼ bu paket deÄŸildir. KanÄ±t Reports/CLEAN_CLONE_VALIDATION.json; ayrÄ±ntÄ± Docs/CLEAN_CLONE-2026-10-25.md. Sonraki paket K2.1 ilk 10/30/60 saniyenin karar yoÄŸunluÄŸu ve tekrar Ã¶lÃ§Ã¼mÃ¼dÃ¼r. Notion tekrar denenmedi; masaÃ¼stÃ¼ aktarÄ±m yedeÄŸi gÃ¼ncellendi.

# K1.6/K1.7 â€” 25 Ekim 2026

K1.6/K1.7: Android derlemesinin dosya varlÄ±ÄŸÄ±na gÃ¶re en yeni sahneyi seÃ§mesi kaldÄ±rÄ±ldÄ±. VarsayÄ±lan aÃ§Ä±k Adventure; CLI -brakeToyProfile ile 10 profil seÃ§ilebilir. AltÄ± eski CheckAndBuild kendi profilini seÃ§er. Eksik/bilinmeyen/Ã§ift hedef hata verir, eski sahneye dÃ¼ÅŸmez ve sahne Ã¼retmez. Yeni APK adaylarÄ± Builds/Android/Candidates altÄ±na gider; yayÄ±mlanmÄ±ÅŸ APK yollarÄ± korunur. Profil PlayerSettings versionName/versionCode deÄŸerlerini geriye almaz. Derleme planÄ± ve iÅŸlemi okunabilir ayrÄ± yapÄ±lardÄ±r. 21 yeni profil testiyle toplam 74/74 EditMode geÃ§ti; gerÃ§ek Unity CLI Adventure varsayÄ±lanÄ± ve Tidy aÃ§Ä±k seÃ§imi exit 0 / ValidatedOnly / buildExecuted=false ile doÄŸrulandÄ±. PlayMode yeniden koÅŸulmadÄ± (Ã¶nceki K1.5 71/71). GerÃ§ek APK/IL2CPP/SDK/imza/kurulum testi yapÄ±lmadÄ±; build sÄ±rasÄ±nda tercihleri finally ile geri alma kodu gerÃ§ek APK koÅŸusunda henÃ¼z sÄ±nanmadÄ±. Notion tekrar denenmedi; masaÃ¼stÃ¼ ek yedek gÃ¼ncellendi. KanÄ±t Reports/BUILD_PROFILE_VALIDATION.json. Sonraki paket K1.2 temiz klon kontrolÃ¼dÃ¼r; cihaz/sanat kabulÃ¼ aÃ§Ä±k kalÄ±r.

# K1.5 â€” 25 Ekim 2026 (yeni APK yayÄ±mlanmadÄ±)

K1.5 tamamlandÄ±: jetpack alÃ§almasÄ±nÄ±n son 0.45 saniyesi korumalÄ±; yakÄ±n iniÅŸ alanÄ± alÃ§alma baÅŸÄ±nda temizleniyor, gÃ¼Ã§ sonundaki 0.85 saniye koruma korunuyor ve bitiyor. Cetvel destek yÃ¼ksekliÄŸinin bir simÃ¼lasyon adÄ±mÄ± geriden gelmesi giderildi. Kitap/cetvel saÄŸ-sol temas sÄ±nÄ±rlarÄ± 24 koÅŸuda, 30/60/120 Hz adÄ±mla denendi; cihaz FPS Ã¶lÃ§Ã¼mÃ¼ deÄŸildir. Yan Ã§Ä±kÄ±ÅŸ, gÃ¼Ã§ bitiÅŸi sonrasÄ± normal Ã§arpÄ±ÅŸma ve aynÄ± rotada retry stoklarÄ± kontrol edildi. Ã–nce 3/5, dÃ¼zeltmeden sonra hedefli 5/5; nihai 53 EditMode +71 PlayMode =124/124. GerÃ§ek kariyer/ghost dosyalarÄ± deÄŸiÅŸmedi. APK ve tablet testi yapÄ±lmadÄ±. Kaynak dal fix/landing-ramp-boundaries. Sonraki kÃ¼Ã§Ã¼k paket K1.6/K1.7 okunabilirlik ve aÃ§Ä±k build hedefi deÄŸerlendirmesidir. KanÄ±t Reports/LANDING_VALIDATION.json, ayrÄ±ntÄ± Docs/LANDING-2026-10-25.md. Notion tekrar denenmedi; notlar masaÃ¼stÃ¼ yedeÄŸine eklendi.

# SaÄŸlamlaÅŸtÄ±rma teslimi â€” 25 Ekim 2026

25 Ekim saÄŸlamlaÅŸtÄ±rma: Relax aynÄ± oyuncaktan tekrar hak kaybetmeyi Ã¶nlemek iÃ§in yakÄ±n yeni oyuncak/prop/rampalarÄ± temizler. Adventure ayrÄ± career-adventure-v1.json kullanÄ±r; eski dosyadan TP, araÃ§lar, gÃ¶rev/baÅŸarÄ±mlar ve harcanmÄ±ÅŸ gÃ¼Ã§ stoklarÄ± korunarak taÅŸÄ±nÄ±r, eski kurallÄ± rekorlar aktarÄ±lmaz. Stil gerÃ§ek ArcadeDirector anahtarÄ±ndan yÃ¼klenir. Ä°lk iki test dÃ¼zeltme Ã¶ncesi baÅŸarÄ±sÄ±z, sonrasÄ± 2/2 geÃ§ti; nihai 53 EditMode +66 PlayMode =119/119. GerÃ§ek kariyer/ghost dosyalarÄ± deÄŸiÅŸmedi. APK derlenmedi. Notion yeni sayfa denemesi blok kotasÄ±ndan reddedildi, sayfa oluÅŸmadÄ±. MasaÃ¼stÃ¼ BrakeToy_Notion_Yedek.txt ek yedek olarak tutulur. AyrÄ±ntÄ± Docs/HARDENING-2026-10-25.md ve Reports/HARDENING_VALIDATION.json. Sonraki paket K1.5 gÃ¼Ã§ bitiÅŸi/iniÅŸ ve rampa-cetvel kenarlarÄ±dÄ±r.

# Proje dÃ¼zeni â€” 25 Ekim 2026

181 dosya Reports/ ve Docs/Archive/pre-v09/ altÄ±na taÅŸÄ±ndÄ±. 150 JSON/XML/PNG/TXT kanÄ±tÄ± bayt dÃ¼zeyinde korundu; mevcut baÄŸlantÄ±lar ve tÃ¼m galeri varlÄ±klarÄ± kontrol edildi. Editor rapor/render araÃ§larÄ± ReportPaths kullanÄ±yor. Unity 6000.3.26f1 batch derlemesi Ã§Ä±kÄ±ÅŸ 0 ile tamamlandÄ±; oynanÄ±ÅŸ testleri yeniden koÅŸulmadÄ± ve APK deÄŸiÅŸmedi. Notion update_page isteÄŸi Ã¼cretsiz blok kotasÄ±nda reddedildi, iÃ§erik oluÅŸturulmadÄ± ve otomatik tekrar yapÄ±lmadÄ±. Tam kayÄ±t GitHub dalÄ±nda; sonraki paket K1.3 Relax Ã§arpÄ±ÅŸma araÅŸtÄ±rmasÄ±dÄ±r. KanÄ±t: Reports/ORGANIZATION_VALIDATION.json.

# Agent rehberleri teslim â€” 24 Ekim 2026

GitHub PR #10 oluÅŸturuldu; docs/agent-handbook kaynaklarÄ± yÃ¼klendi. Main README8da3c121c12bdb9010963896e342ddd052707551 ile yedi belgeye ve PR'a yÃ¶nlendirildi, API geri okumasÄ± doÄŸrulandÄ±. HiÃ§bir oyun PR'Ä± merge edilmedi. Yedi dosya300 satÄ±r altÄ±nda; eski dÃ¶rt kaynak arÅŸivde. Notion mevcut altÄ± sayfada gÃ¼ncel Ã¶zet/baÄŸlantÄ± kapsamÄ±yla doÄŸrulandÄ±,33 eski alt sayfa korundu; yedi tam yeni kopya blok kotasÄ±nda bekliyor. Teslim kapsamÄ± Docs/AGENT_DOCUMENTATION_DELIVERY.json iÃ§inde. Bu turda oyun kodu/APK deÄŸiÅŸmedi ve Unity testleri tekrar Ã§alÄ±ÅŸtÄ±rÄ±lmadÄ±.

# Agent belge teslimi ve Notion sonucu â€” 24 Ekim 2026

Yedi dosya sÄ±rasÄ±yla116/146/161/165/12/78/123 satÄ±r; UTF-8/link/satÄ±r doÄŸrulamasÄ± geÃ§ti, oyun kaynaklarÄ± deÄŸiÅŸmedi. Kaynak4c61e0f docs/agent-handbook dalÄ±na yÃ¼klendi. Notion async kuyruÄŸu503 dÃ¶ndÃ¼ÄŸÃ¼ iÃ§in geri okuma gerektiren senkron gÃ¼ncellemeye geÃ§ildi ve mevcut altÄ± sayfa gÃ¼ncellendi/geri okundu. Yeni yedi sayfa isteÄŸi Ã¼cretsiz blok kotasÄ±ndan reddedildi; otomatik create tekrarÄ± yapÄ±lmadÄ±. Merkez33 tarihsel alt sayfayÄ± koruyor; dÃ¶rt gÃ¼ncel konu Ã¶zeti ve tam GitHub kaynak baÄŸlantÄ±larÄ± mevcut. Tam ayrÄ± kopyalar pending; Docs/NOTION_INDEX.json ve NOTION_SYNC-agent-handbook.json kapsamÄ± ayÄ±rÄ±r. Ã–nceki112 Unity testi yeniden koÅŸulmadÄ±, yeni APK oluÅŸturulmadÄ±.

# Agent belge paketi â€” 24 Ekim 2026

KullanÄ±cÄ± hazÄ±rlama ve Notion gÃ¼ncellemesine izin verdi. AGENTS yeniden yazÄ±ldÄ±; YOL_HARITASI/MIMARI/MEKANIKLER ile CLAUDE/KARARLAR/DOGRULAMA eklendi. Eski belgeler arÅŸivlendi, Ä°ngilizce kaynaklar tarihsel olarak iÅŸaretlendi. Oyun kodu/sahne/paket deÄŸiÅŸmedi; 112 test Ã¶nceki kanÄ±ttÄ±r, bu tur yeniden koÅŸulmadÄ±. Relax yeni havuz temizliÄŸi, ortak kariyer rekor ayrÄ±mÄ± ve benchmark generator uyumu doÄŸrulama adayÄ± olarak kaydedildi; dÃ¼zeltilmiÅŸ sayÄ±lmaz. Notion eriÅŸim haritasÄ± create/update kullanÄ±labilir dÃ¶ndÃ¼; gerÃ§ek eÅŸitleme sonucu ayrÄ±ca kaydedilecek.

# v0.9 teslim edildi â€” 24 Ekim 2026

GitHub PR #9 (base fix/table-layout-v081), v0.9.0 Release ve APK yÃ¼klendi. GitHub asset digest/byte sayÄ±sÄ± yerel APK ile aynÄ±; main README d6dae250c0752a9c41830ddf479ce42d155c55d8 doÄŸrulandÄ±. Kaynak dal feat/adventure-v09; oyun PR'larÄ± merge edilmedi, main oyun kodu Faz1 olarak aÃ§Ä±klanÄ±r. DELIVERY-v09.json teslim kanÄ±tÄ±dÄ±r. PC Unity penceresi bÃ¼yÃ¼tÃ¼ldÃ¼, ToyRoomAdventure Play menÃ¼sÃ¼ 1Ã— hazÄ±r bÄ±rakÄ±ldÄ±; kullanÄ±cÄ± turu baÅŸlatmadÄ±. Kariyer korunur. Yeni fiziksel tablet testi ve insan sanat/oynanÄ±ÅŸ kabulÃ¼ bekler. Notion yeni kayÄ±tlarÄ± bilinen Ã¼cretsiz blok kotasÄ± nedeniyle yerel/GitHub pending; yeniden blok isteÄŸi yapÄ±lmadÄ±.

# v0.9 Android doÄŸrulandÄ± â€” 24 Ekim 2026

Kaynak b656017cb48c7b10b62891647135d0bc2371cfde; 0.9.0/kod10 ARM64 IL2CPP non-Development, 40568255 bayt. V2/aynÄ± sertifika, ZIP bÃ¼tÃ¼nlÃ¼ÄŸÃ¼/ZIP16KB/ELF16KB geÃ§ti. Fiziksel tablet kabulÃ¼ bekler. Rapor ANDROID_BUILD_REPORT-v09.json. GitHub Release yÃ¼kleme sÄ±rada; Notion kotasÄ± pending.

# v0.9 checkpoint â€” 24 Ekim 2026

112/112 test geÃ§ti. Misket erken giriÅŸ hatasÄ± hÄ±zla Ã¶lÃ§eklenen baÅŸlangÄ±Ã§ mesafesiyle dÃ¼zeltildi; son12 uzun koÅŸuda Ã§arpÄ±ÅŸma/hak kaybÄ±/havuz tÃ¼kenmesi yok. KurmalÄ± kurbaÄŸa/Ã¶rdek, gerÃ§ek enine tren, vinÃ§ geÃ§iÅŸ boÅŸluÄŸu ve sabit uzunluklu halat; tÃ¼nel yÃ¼ksek tavan/duvar Ã§arpÄ±ÅŸmalarÄ±. Malzeme baÅŸÄ±na statik mesh birleÅŸtirme. Fiziksel tablet ve sanat kabulÃ¼ bekler. Notion kotasÄ± nedeniyle yeni notlar yerelde bekliyor.

# v0.9 started â€” 2026-10-24
Eight mechanics authorized; PLAN-v0.9.md. New scene preserves Tidy, no domino, same difficulty curve. Implementation/testing in progress; no acceptance claimed. Notion quota pending.

## 24 Ekim â€” v0.8.1 yerleÅŸim dÃ¼zeltmesi

Yeni ToyRoomTidy sahnesi, fix/table-layout-v081 dalÄ±. Mavi parÃ§alar v0.8 oyuncak kasabasÄ± kenar dekorlarÄ±ydÄ±; 42 m dekor dÃ¶ngÃ¼sÃ¼yle 64 m defter/kalemlik dÃ¶ngÃ¼sÃ¼ baÄŸÄ±msÄ±z olduÄŸundan tekrar tekrar Ã§akÄ±ÅŸÄ±yordu. Yeni sahnede iki grup aynÄ± 64 m dÃ¶ngÃ¼yÃ¼ ve ayrÄ±lmÄ±ÅŸ boÅŸluklarÄ± kullanÄ±r. Defter/kalemlik en fazla 1.25 birim geniÅŸliÄŸe indirildi, Â±4.45 kenar merkezlerine oturtuldu; oyun alanÄ±na ve masa dÄ±ÅŸÄ±na taÅŸmalarÄ± sÄ±nandÄ±. TÃ¼m bÃ¶lge parÃ§alarÄ± hÃ¼cre olarak birlikte akar, pencere/ev parÃ§alarÄ± ayrÄ±ÅŸmaz.

Radyo eski birleÅŸik masa mesh'inden ayrÄ±lÄ±p soldaki oyuncak rafÄ±nÄ±n Ã¼stÃ¼ne taÅŸÄ±ndÄ±; alt yÃ¼zeyi gerÃ§ek raf Ã¼stÃ¼nden 0.025 birim yukarÄ±da. SÃ¼rÃ¼ÅŸ mesafesiyle hareket etmez. Domino model/Ã¼retim/ayar dÃ¼ÄŸmesi aktif Tidy sahnesinden kaldÄ±rÄ±ldÄ±; yerine mevcut rampa fÄ±rsatÄ± kullanÄ±lÄ±r. Ã–nceki v0.8 sahnesi korunur. HÄ±z eÄŸrisi, karÅŸÄ±laÅŸma aralÄ±ÄŸÄ±, gÃ¼Ã§ sÃ¼releri/fiyatlarÄ± ve kontrol ayarlarÄ± deÄŸiÅŸtirilmedi. Kariyer/TP/araÃ§/gÃ¼Ã§ haklarÄ± aynÄ± dosyada korunur; geometrisi farklÄ± eski parkurun ghost'u yeni sahnede yÃ¼klenmez.

**104/104 Unity testi** (48 EditMode +56 PlayMode) geÃ§ti. ÃœÃ§ bÃ¶lge, dÃ¶rt tam ortak dÃ¶ngÃ¼ ve yarÄ±m metre adÄ±mlÄ± yerleÅŸim Ã¶rnekleri; raf desteÄŸi/radyo sabitliÄŸi; domino yokluÄŸu ve eski 102 regresyon dahil. Fiziksel cihaz ve insan gÃ¶rÃ¼ntÃ¼/oynanÄ±ÅŸ kabulÃ¼ bekler. Notion yeni iÃ§erik iÅŸlemi Ã¶nceki Ã¼cretsiz blok sÄ±nÄ±rÄ± nedeniyle engelli; otomatik tekrar yapÄ±lmadÄ±, yerel/GitHub notlarÄ± gÃ¼ncel.

## 24 Ekim â€” v0.8 Playground

Yeni ToyRoomPlayground sahnesi ve feat/playground-v08 dalÄ±; v0.7 korunur. GÃ¼venli/riskli yol, baÄŸlÄ± altÄ± karÅŸÄ±laÅŸma ailesi, isteÄŸe baÄŸlÄ± domino ile yol aÃ§ma, sinyalli aÃ§Ä±lÄ±p kapanan oyuncak garaj kapÄ±sÄ±/hareketli oyuncak geÃ§iÅŸleri, Ã¼Ã§ yatay konuma daÄŸÄ±lan kitap rampalarÄ± ve farklÄ± hareketleri baÄŸlayan ustalÄ±k bonusu uygulandÄ±. Rampa artÄ±k dar merkez toleransÄ± yerine aracÄ±n teker/gÃ¶vde temas alanÄ±nÄ± kullanÄ±r; yan giriÅŸ/Ã§Ä±kÄ±ÅŸ ve kenarlarÄ± sÄ±nandÄ±.

3 sn yavaÅŸlatma, 3 sn korumalÄ± roket, 5 sn jetpack. Ä°lk kariyere Ã¼Ã§er kalÄ±cÄ± hak; menÃ¼de kullanÄ±m baÅŸÄ±na 350/500/650 TP yenileme. Retry hak vermez; pause sÃ¼reyi durdurur. GerÃ§ek para/coin/IAP yok. YardÄ±mlÄ± tur etiketlenir; gÃ¼Ã§ kullanÄ±ldÄ±ÄŸÄ±nda kiÅŸisel ghost gizlenir ve yeni ghost yazÄ±lmaz. Yeni rekor/kariyer dosyasÄ± ayrÄ±; TP/aÃ§Ä±lmÄ±ÅŸ araÃ§lar taÅŸÄ±nÄ±r.

Masa bÃ¶lgeleri ve domino Ayarlar'dan ayrÄ± kapatÄ±lÄ±r. Oda Ä±ÅŸÄ±ÄŸÄ±nda yÃ¶n/kontrast, plastik-kumaÅŸ-kauÃ§uk-metal yÃ¼zey ayrÄ±mÄ±; odanÄ±n Ã¼st kÄ±smÄ±nÄ± aÃ§Ä±k bÄ±rakan menÃ¼, simgeli gÃ¶rev/rozet kartlarÄ± ve farklÄ± araÃ§ siluetleri eklendi. MÃ¼zik korunur. AraÃ§lar ayrÄ±ntÄ±lÄ± son model deÄŸil, sonraki model agent Ã§alÄ±ÅŸmasÄ±na temel siluetlerdir.

Son Unity: **102/102** (48 EditMode +54 PlayMode). Fiziksel v0.8 FPS/p95/Ä±sÄ±nma ve insan sanat/oynanÄ±ÅŸ kabulÃ¼ bekler. CanlÄ± online ve maÄŸaza kabulÃ¼ yok. Notion yeni sayfa oluÅŸturma Ã¼cretsiz blok sÄ±nÄ±rÄ±ndan reddedildi; NOTION_PENDING-v08.md iÃ§inde eÅŸitleme kaydÄ± tutulur.

# 23 Ekim 2026 â€” Faz 6 revizyon adayÄ± kontrol noktasÄ±

## v0.7 Joyful Room â€” uygulama ve gÃ¶rsel inceleme

KullanÄ±cÄ±nÄ±n ses/oynanÄ±ÅŸ/oda revizyonu PLAN-v0.7.md ile baÅŸlatÄ±ldÄ±. 5b9671b plan/araÅŸtÄ±rma; 7a7b993 ilk Ã§alÄ±ÅŸan uygulama ve 84 test ile GitHub'a gÃ¶nderildi. Notion 32 revizyon/araÅŸtÄ±rma sayfasÄ± ve gÃ¼nlÃ¼k gÃ¼ncellendi. v0.6 korunuyor.

GerÃ§ek CC0 menÃ¼/yarÄ±ÅŸ mÃ¼zikleri, plastik Ã§ocuk kornasÄ± ve oyuncak ÅŸakÄ±rtÄ±sÄ±; aÃ§Ä±klamalÄ± Ã¼Ã§ ses kanalÄ±; hÄ±zlÄ± hÄ±z eÄŸrisi, recent-pattern hafÄ±zasÄ±, farklÄ± oyuncak/stagger/jitter, falsolu ve ikili/Ã¼Ã§lÃ¼ top, dÃ¶nen tekerlekli bus, kitap rampasÄ±/havada TP, yeni oyuncaklar ve gÃ¼ndÃ¼z/gece odasÄ± uygulandÄ±. Yeni kariyere sahip olunan Ã¼rÃ¼n/TP taÅŸÄ±nÄ±r, deÄŸiÅŸen skor ve X+Y ghost dosyalarÄ± ayrÄ±dÄ±r. v0.7 review-r1 sunucusuna baÄŸlanmaz.

GerÃ§ek render incelemesinde dÃ¼z masa/boÅŸ duvar, radyo omuz aÃ§Ä±klÄ±ÄŸÄ± ve hÄ±zlÄ± toplarÄ±n duvar arkasÄ±nda geÃ§ gÃ¶rÃ¼nmesi yakalandÄ±. Masa mikro doku/kenarlarÄ±, pastel oda duvarÄ±, bunting/gezegen mobil ve daha derin arka oda hazÄ±rlandÄ±. Maksimum ileri top yaklaÅŸÄ±mÄ± iÃ§in gÃ¶rÃ¼nÃ¼rlÃ¼k ve model vertex/collider sÄ±nÄ±r testi eklendi. UÃ§uÅŸ pozunda dÃ¶nÃ¼ÅŸÃ¼n kare kare birikmesi ayrÄ±ca bulundu; X aÃ§Ä±sÄ± mutlak hesaplanarak dÃ¼zeltildi, pause/iniÅŸ regresyonu geÃ§ti. Son geÃ§it/rampa kalibrasyonu sonrasÄ± test/render tekrar koÅŸusu sÃ¼rÃ¼yor. HenÃ¼z yeni APK ve kullanÄ±cÄ± ses/sanat/cihaz kabulÃ¼ yok.

## Son teslim ve eÅŸitleme

Kaynak ca7aa2ab18094269f6d2e9d0d9aed7f87d3321b6, PR #5 ve v0.6.0 GitHub'a gÃ¶nderildi. GitHub APK digest yerel 65750071216c7742625eab7f8e410f027a815f96741d3518c6eecb6821ead042 ile aynÄ±; 38.232.631 bayt. v0.5 ile aynÄ± Debug sertifikasÄ± doÄŸrulandÄ±. Main README 179f9370750dc89e1503292c6e0fbc131e418894 gÃ¼ncel teslim ve ayrÄ± kaynak dalÄ±nÄ± aÃ§Ä±kÃ§a gÃ¶sterir. PR'lar birleÅŸtirilmedi.

Notion kÃ¶kÃ¼ 31 alt sayfayla korundu; 28â€“31 plan/geri bildirim/servis/teslim sayfalarÄ± ayrÄ±. GÃ¼nlÃ¼k, README/tasarÄ±m/mimari/yol haritasÄ±/test/deÄŸiÅŸiklik, cihaz, Android, Unity kanÄ±tÄ± ve model sayfalarÄ± gÃ¼ncellendi. Teslim sayfasÄ± tekrar okunup APK SHA256/gÃ¶rsel/eski bekleme metninin kaldÄ±rÄ±lmasÄ± doÄŸrulandÄ±. Fiziksel v0.6, Ä±sÄ±, 16KB gerÃ§ek ortam, canlÄ± servis/kalÄ±cÄ± online kimlik ve maÄŸaza kabulÃ¼ aÃ§Ä±k. Tablet dosya kopyasÄ± iddia edilmedi; doÄŸrudan APK linki saÄŸlandÄ±.

KullanÄ±cÄ± tÃ¼m baÄŸÄ±msÄ±z geliÅŸtirmeye devam izni verdi. v0.5 tablet geri bildirimi alÄ±ndÄ±: kontrol iyi, tam grafikte Ã¶znel akÄ±cÄ±lÄ±k; ilk 30 sn/parkur Ã§ok kolay ve sÄ±ÄŸ, mÃ¼zik eksik, sanat hedefin altÄ±nda. FPS/p95 ve kesinti Ã¶lÃ§Ã¼lmedi. KullanÄ±cÄ± bu kalite revizyonlarÄ±nÄ± sonraki tura ayÄ±rdÄ±.

feat/complete-review-build Ã¼zerinde Review sahnesi, 10 araÃ§ varyantÄ±, garaj, kariyer/ekonomi, kozmetik, 3 gÃ¶rev/15 baÅŸarÄ±m, Relax, ghost/gÃ¼nlÃ¼k ve REST sÄ±nÄ±rÄ± kodlandÄ±. 39 EditMode +36 PlayMode geÃ§ti. 9 Python test ve gerÃ§ek C#HTTP yerel probe geÃ§ti. On araÃ§ ve tÃ¼m yeni menÃ¼ler motor renderÄ±yla incelendi. 316 renderer/133 mesh/768322 Ã¼Ã§gen tÃ¼m inaktif araÃ§/havuz dahil; eksik referans/shader/dekor collider sÄ±fÄ±r. Ä°lk yanlÄ±ÅŸ mesh testi ve DX12 kapanÄ±ÅŸÄ± dÃ¼zeltildi, son test/yakalama baÅŸarÄ±lÄ±.

Notion 28 plan, 29 geri bildirim, 30 servis sayfalarÄ± oluÅŸturuldu; gÃ¼nlÃ¼k gÃ¼ncellendi. CanlÄ± servis yok, offline varsayÄ±lan kapÄ±sÄ± korunuyor. Android non-Development test paketi derleniyor; paket/son teslim kanÄ±tÄ± sonraki gÃ¼nlÃ¼ÄŸe eklenecek.

# GeliÅŸtirme gÃ¼nlÃ¼ÄŸÃ¼

## 21 Ekim 2026 â€” Ä°lk sÃ¼rÃ¼m ve gÃ¶rsel yÃ¶n

- Faz 1: gerÃ§ek 3D sÃ¼rÃ¼ÅŸ, mouse/touch, kayan dÃ¼nya, swept collision, retry ve pause oluÅŸturuldu.
- Unity 6000.3.26f1: 19 EditMode ve 4 PlayMode testi geÃ§ti. AyrÄ± .NET Ã§alÄ±ÅŸtÄ±rÄ±cÄ±sÄ±nda aynÄ± 19 Ã§ekirdek kontrolÃ¼ ve 10.000 Ã§arpÄ±ÅŸma Ã¶rneÄŸi geÃ§ti.
- Android ARM64 IL2CPP Development APK Ã¼retildi ve imzasÄ±/arÅŸivi doÄŸrulandÄ±. Fiziksel cihaz performansÄ± doÄŸrulanmadÄ±.
- KullanÄ±cÄ± ilk sÃ¼rÃ¼mÃ¼ denedi; Ã§ocuk odasÄ± hissini yetersiz buldu ve odadaki masa Ã¼zerinde sÃ¼rÃ¼ÅŸ istedi. Bu geri bildirim kontrol hissinin onaylandÄ±ÄŸÄ± anlamÄ±na gelmez.
- Masa/oda kompozisyonu iÃ§in imagegen konsepti oluÅŸturuldu; kullanÄ±cÄ± gÃ¶rÃ¼nÃ¼mÃ¼ onayladÄ± ve sÄ±radaki sÃ¼rÃ¼mÃ¼ yetkilendirdi.

## 21 Ekim 2026 â€” GitHub ve Notion baÄŸlantÄ±sÄ±

- GitHub hesabÄ±nda mevcut brscncn1/Brake-Toy deposu bulundu. BaÅŸlangÄ±Ã§ README commit'i korunarak yerel proje geÃ§miÅŸi birleÅŸtirildi; Faz 1 kaynaklarÄ± ve onaylanan konsept main dalÄ±na gÃ¶nderildi.
- Yeni Ã§alÄ±ÅŸma dalÄ±: feat/toy-room-tabletop.
- Notion baÄŸlantÄ±sÄ± Ã¶nce gÃ¶rÃ¼nmÃ¼yordu; baÄŸlantÄ± kullanÄ±labilir olduÄŸunda arama ile doÄŸrulandÄ±. BRAKE TOY â€” Proje Merkezi Ã¶zel sayfasÄ± oluÅŸturuldu; belgeler ayrÄ± alt sayfalara aktarÄ±lÄ±yor.
- KullanÄ±cÄ±nÄ±n isteÄŸi doÄŸrultusunda bundan sonraki Ã¶nemli karar, uygulama, test ve teslim sonuÃ§larÄ± yerel gÃ¼nlÃ¼k ve Notion gÃ¼nlÃ¼ÄŸÃ¼ne kaydedilecek.
- Aktif iÅŸ: odanÄ±n 3D kabuÄŸu, mobilyalar, ahÅŸap masa, oda gÃ¶rÃ¼nÃ¼rlÃ¼ÄŸÃ¼nÃ¼ koruyan kamera, geliÅŸtirilmiÅŸ Sparky ve oynanabilir yeni sahne.

## 21 Ekim 2026 â€” Oda ve masa sahnesi Ã¼retildi

- ToyRoomTabletop.unity ayrÄ± bir sahne olarak Unity iÃ§inde Ã¼retildi. Eski ToyRoom ve Faz 1 prefablarÄ± korundu.
- Masa yÃ¼zeyi/kenarlarÄ±/ayaklarÄ±, oda zemini/duvarlarÄ±, pencere/perdeler, raf/oyuncak ayÄ±, yatak, poster, sandalye ve kÄ±rtasiye dekorlarÄ± eklendi.
- Yeni Sparky ve Ã¼Ã§ engel tipi yuvarlatÄ±lmÄ±ÅŸ mesh'lere geÃ§ti. Tekerlek animasyonu yalnÄ±zca sÃ¼rÃ¼ÅŸte Ã§alÄ±ÅŸÄ±r. Gameplay collider ve kontrol parametreleri korundu.
- 20 derecelik kamera ile ilk gerÃ§ek Unity Play Mode renderÄ± incelendi. Oda gÃ¶rÃ¼nÃ¼r hale geldi; bÃ¼yÃ¼k yÃ¼zeylerdeki bevel normal interpolasyonu dÃ¼zeltilerek koyu bantlar giderildi. AhÅŸap deseni sadeleÅŸtirildi.
- Masa geniÅŸliÄŸi oda zemini ve yan mobilyalarÄ± gÃ¶stermek iÃ§in 12 birime ayarlandÄ±; kullanÄ±labilir sÃ¼rÃ¼ÅŸ alanÄ± 7 birim olarak korundu.
- Yeni sahne iÃ§in Ã¼Ã§ PlayMode regresyon testi yazÄ±ldÄ±; bu aÅŸamada henÃ¼z Ã§alÄ±ÅŸtÄ±rÄ±lmadÄ±.
- GitHub Releases Ã¼zerinde v0.1.0 APK ve derleme raporu yayÄ±mlandÄ±. Yeni APK henÃ¼z Ã¼retilmedi.

## 21 Ekim 2026 â€” v0.2 doÄŸrulama ve teslim hazÄ±rlÄ±ÄŸÄ±

- 20Â°/28Â° gerÃ§ek Unity renderlarÄ± karÅŸÄ±laÅŸtÄ±rÄ±ldÄ±; oda gÃ¶rÃ¼nÃ¼rlÃ¼ÄŸÃ¼ iÃ§in 20Â° seÃ§ildi.
- YakÄ±n masa dekorlarÄ± WorldScroller'Ä±n gerÃ§ek kayma/reset olaylarÄ±na baÄŸlandÄ±; pause ve Ã§arpÄ±ÅŸma kesriyle tutarlÄ± ilerler.
- Unity 27/27 test geÃ§ti: 19 EditMode, 8 PlayMode. Yeni dekor akÄ±ÅŸÄ±/reset testi dahil.
- v0.2 Android ARM64 IL2CPP Development APK Ã¼retildi; boyut 37.0 MiB. Cihaz Ã§alÄ±ÅŸtÄ±rma/performance testi yapÄ±lmadÄ±.
- SÃ¼rÃ¼m raporlarÄ± ve Ã¶nceki raporlarÄ±n v0.1 arÅŸivi hazÄ±rlandÄ±. Kaynak/Notion/Release son eÅŸitlemesi teslimden Ã¶nce doÄŸrulanacak.

## v0.2 yayÄ±n ve Notion eÅŸitlemesi tamamlandÄ±

- Kaynaklar feat/toy-room-tabletop dalÄ±na gÃ¶nderildi; PR #1 aÃ§Ä±ldÄ±: https://github.com/brscncn1/Brake-Toy/pull/1
- v0.2.0 Ã¶n sÃ¼rÃ¼mÃ¼ yayÄ±mlandÄ±; APK, derleme raporu ve gerÃ§ek Unity gÃ¶rÃ¼ntÃ¼sÃ¼ yÃ¼klendi. Uzak APK Ã¶zeti yerelle eÅŸleÅŸiyor.
- Notion proje durumu ve etkilenen 10 belge/test sayfasÄ± gÃ¼ncellendi; 16 â€” v0.2 Masa ve oda sÃ¼rÃ¼mÃ¼ oluÅŸturuldu. GeliÅŸtirme gÃ¼nlÃ¼ÄŸÃ¼ne gerÃ§ek test/yayÄ±n sonuÃ§larÄ± eklendi.
- Unity ToyRoomTabletop sahnesiyle tekrar aÃ§Ä±ldÄ±. GerÃ§ek cihaz ve kullanÄ±cÄ± gÃ¶rsel geri bildirimi bekleniyor.

## v0.3 araÅŸtÄ±rma ve uygulama baÅŸladÄ±

- KullanÄ±cÄ± erken heyecan/hÄ±z, daha gerÃ§ekÃ§i modeller ve yuvarlanan toplarÄ± yetkilendirdi. Yeni feat/arcade-workshop-v03 dalÄ± aÃ§Ä±ldÄ±.
- Birincil Halfbrick/GDC, Milestone, SYBO ve Unity kaynaklarÄ± incelendi; kapsam ve Ã§Ä±karÄ±mlar RESEARCH-v0.3.md iÃ§inde. TÃ¼m yayÄ±nlarÄ±n tarandÄ±ÄŸÄ± veya video izlendiÄŸi iddia edilmiyor.
- AyrÄ± ToyRoomArcade sahnesi oluÅŸturuldu: seedli satÄ±rlar, 24 tehlike havuzu, hÄ±z eÄŸrisi, sÃ¼rekli hareketli top Ã§arpÄ±ÅŸmasÄ±, Near Miss/kombo/rekor, HUD/ses.
- Ã–zgÃ¼n diecast araÃ§ geometrisi ve daha detaylÄ± oyuncaklar; Poly Haven CC0 ahÅŸap diffuse/normal, ayrÄ± renderer/SSAO/reflection. Topun merkez Ã§evresinde dÃ¶nmesi dÃ¼zeltildi.
- 128.000 satÄ±r/10.000 Ã§arpÄ±ÅŸma Ã¶rneÄŸi ve motor iÃ§i regresyon testleri baÅŸlatÄ±ldÄ±; sonuÃ§lar henÃ¼z doÄŸrulanmadÄ±.
- Notion 17 â€” v0.3 AraÅŸtÄ±rma ve uygulama planÄ± oluÅŸturuldu.

## v0.3 son doÄŸrulama ve paket

- 26 EditMode + 15 PlayMode = 41/41 Unity testi geÃ§ti. Ä°ki dakikalÄ±k tanÄ±k sÃ¼rÃ¼ÅŸÃ¼, 128.000 satÄ±r ve 10.000 top Ã¶rneÄŸi dahil.
- Son yakÄ±n planlarda far/kaput-lastik kesiÅŸmesi ve gÃ¶vde normalleri dÃ¼zeltildi; gerÃ§ek renderlar incelendi. Sabit oda birleÅŸtirildi, pencere aÃ§Ä±klÄ±ÄŸÄ± ve gÃ¶lgesiz dolgu eklendi.
- Android ARM64 IL2CPP APK oluÅŸturuldu; imza/paket/arÅŸiv kontrolÃ¼ geÃ§ti. Fiziksel cihaz ve kullanÄ±cÄ± hissi onayÄ± bekliyor.
- GitHub yayÄ±n ve Notion son eÅŸitlemesi hazÄ±rlanÄ±yor.

## v0.3 GitHub ve Notion teslimi tamamlandÄ±

- Kaynak feat/arcade-workshop-v03 dalÄ±na gÃ¶nderildi; PR #2: https://github.com/brscncn1/Brake-Toy/pull/2
- v0.3.0 Ã¶n sÃ¼rÃ¼m APK/rapor/model kanÄ±tÄ± ve gerÃ§ek renderlarla yayÄ±mlandÄ±. Uzak APK SHA256 yerelle eÅŸleÅŸiyor: a6a9cbdb0aa321f6e54acb744654c23b75b49eda272dabf3dde0cfb0d73d4ed7.
- Notion proje merkezi ve etkilenen belgeler eÅŸitlendi; 17 araÅŸtÄ±rma, 18 teslim, 19 varlÄ±k haklarÄ±, 20 model kanÄ±tÄ± sayfalarÄ± mevcut. GerÃ§ek sÃ¼rÃ¼ÅŸ/Fever/model gÃ¶rselleri kaynak commitine sabitlenmiÅŸ URLlerle eklendi.
- Son kabul kanÄ±tÄ± 41/41 Unity testi; fiziksel telefon ve insan oyuncu tempo/kalite geri bildirimi bekleniyor. Unity ToyRoomArcade sahnesiyle aÃ§Ä±ldÄ±.

## v0.4 kalite incelemesi ve Ã§alÄ±ÅŸma baÅŸlangÄ±cÄ±

- KullanÄ±cÄ± sonraki aÅŸama Ã¶ncesi kapsam, Ã¶z eleÅŸtiri ve Ã¶neriler istedi. GerÃ§ek v0.3 gÃ¶rÃ¼ntÃ¼leri/kodlarÄ± incelendi; QUALITY_REVIEW-v0.3.md ve PLAN-v0.4.md yazÄ±ldÄ±.
- Ã–ncelik kamera/UI, malzeme/Ä±ÅŸÄ±k ve araÃ§ oranlarÄ±, engel kataloÄŸu/Ã§eÅŸitlenen turlar ve grafik profilleri. Ses/Ã§arpÄ±ÅŸma cilasÄ± daha sonra; garaj/yeni harita/online bu sÃ¼rÃ¼mde yok.
- feat/refined-room-v04 dalÄ± aÃ§Ä±ldÄ±; Notion 21/22 sayfalarÄ± ve gÃ¼nlÃ¼k gÃ¼ncellendi. HenÃ¼z v0.4 test sonucu yok.

## KullanÄ±cÄ±nÄ±n kapsam kararÄ± â€” tÃ¼m Ã¶neriler bu sÃ¼rÃ¼mde

Kalite raporundaki dokuz baÅŸlÄ±k aynÄ± v0.4 Ã§alÄ±ÅŸmasÄ±na alÄ±ndÄ±. Ses ve Ã§arpÄ±ÅŸma cilasÄ± sonraya bÄ±rakÄ±lmayacak: katmanlÄ± motor/teker sesi, kuru oyuncak Ã§arpÄ±ÅŸmasÄ± ve kÄ±sa gÃ¶rsel tepki. Oda ayrÄ±ntÄ±larÄ± ve gerÃ§ek motor Ã¶nce/sonra karÅŸÄ±laÅŸtÄ±rmasÄ± da teslimde yer alacak. Fiziksel cihaz FPS/Ä±sÄ±/dokunma kabulÃ¼ iÃ§in cihaz matrisi ve Ã¶lÃ§Ã¼m kaydÄ± hazÄ±rlanacak; cihaz olmadan Ã¶lÃ§Ã¼ldÃ¼ denmeyecek. Ã–nceki dar kapsam ifadeleri bu kararla gÃ¼ncellendi. Yeni harita/garaj/online bu kalite raporunun Ã¶nerileri arasÄ±nda deÄŸildir.

## v0.4 implementation and first checks

- User expanded the milestone to all nine review recommendations, including audio/impact polish. Separate refined scene/assets preserve previous versions and real user records.
- Added defined pooled encounters, varied gaps/ball slopes/events and new-run/same-route retry. Catalog prefabs instantiate once at scene startup; no new hazards while driving.
- Added private runtime high/low pipeline copies, menu quality controls, local opt-in performance recorder and explicit development benchmark. No USB-debuggable device detected; user could not locate the setting, so delivery proceeds with direct tablet APK testing.
- Reduced wood contrast and adjusted tint after the first render looked washed out, softened sunlight, refined cabin/roof, opened wheel arches and added window joinery/plant. Actual Unity front/rear/game renders inspected; concept still exceeds current art quality.
- Layered motor/friction/ratchet, dry wood/metal and rubber impact cues; short grounded wobble/camera impulse synchronized with impact and delayed result panel.
- First tests: 28/28 EditMode +25/25 PlayMode. Final catalog/art/audio changes are being rechecked before Android packaging.
- Updated GitHub main landing README; fixed a local text-encoding conversion before delivery. All further Python file IO uses explicit UTF-8.

## v0.4 final paket kontrolÃ¼

Masa chunk uÃ§larÄ±ndaki bevel ve duvar panel Ã§izgileri gÃ¶rÃ¼ldÃ¼; yalnÄ±zca yeni sahnenin kesintisiz yÃ¼zey geometrisiyle giderildi. PlayMode 25/25 yeniden geÃ§ti; model denetimi 61 unique mesh /192 renderer /158146 Ã¼Ã§gen (inaktif havuz dahil), hata sÄ±fÄ±r. APK yeniden derlendi ve imza/paket/ZIP denetimi geÃ§ti. SHA256: 0ce3f84da0b58b3e98dc0fab0f1922985fcd194bb22c017b6f172e45f6dca4a0. KullanÄ±cÄ±nÄ±n tableti Windows MTP aygÄ±tÄ± olarak gÃ¶rÃ¼nÃ¼yor; APK aktarÄ±mÄ± yapÄ±lÄ±yor, henÃ¼z tablet oynanÄ±ÅŸ/FPS kabulÃ¼ yok.

## v0.4 yayÄ±mlandÄ± ve eÅŸitlendi

Kaynak commit 7e36334d85211c5baa3ce1a42440c17e92043f6b, PR https://github.com/brscncn1/Brake-Toy/pull/3 (v0.3 dalÄ± Ã¼zerine). v0.4.0 prerelease APK/render/raporlarÄ± yayÄ±mlandÄ±; GitHub APK asset digest yerel SHA256 ile eÅŸleÅŸti. Ana sayfa README 517c4df ile v0.4 durumuna gÃ¼ncellendi; main kaynak tabanÄ±nÄ±n Ã¶nceki prototip olduÄŸu ve yeni kaynak dalÄ± aÃ§Ä±kÃ§a belirtildi.

APK Huawei MatePad 11 /Dahili depolama/Download altÄ±nda gÃ¶rÃ¼nÃ¼yor; kullanÄ±cÄ±ya kurulum ve isteÄŸe baÄŸlÄ± 60 sn sÃ¼rÃ¼ÅŸ Ã¶lÃ§Ã¼mÃ¼ adÄ±mlarÄ± verildi. Fiziksel kabul henÃ¼z alÄ±nmadÄ±. Notion 24 alt sayfayla korundu; 21 eleÅŸtiri/22 plan gÃ¼ncellendi, 23 kalite sonuÃ§larÄ± ve24 cihaz kabulÃ¼ oluÅŸturuldu. Ä°lgili kurulum/tasarÄ±m/mimari/yol haritasÄ±/gÃ¶rsel/test/deÄŸiÅŸiklik/lisans/model/derleme kanÄ±tÄ± sayfalarÄ± gÃ¼ncellendi; root ve gÃ¶rsel/teslim sayfalarÄ± tekrar okunarak doÄŸrulandÄ±. Eski kullanÄ±cÄ± iÃ§erikleri/alt sayfalar silinmedi.

## v0.5 baÅŸladÄ±

KullanÄ±cÄ± sonraki aÅŸama ile altÄ± Ã§Ã¶zÃ¼mÃ¼n birlikte yapÄ±lmasÄ±nÄ± onayladÄ±. Dal feat/living-workshop-v05. Aktif Faz 3 ve Faz 2 kapanÄ±ÅŸÄ±, ardÄ±ndan Faz 4/5/6 olmak Ã¼zere mevcut dahil dÃ¶rt ana Ã§alÄ±ÅŸma bloÄŸu aÃ§Ä±klandÄ±; sÃ¼rÃ¼m/tarih garantisi verilmedi. Kaynak araÅŸtÄ±rmasÄ±: Poly Haven Painted Wooden Chair02/Cotton Jersey/Studio Small09, Kenney Impact Sounds, qubodup marble rolling, CC0. Ã–nceki v0.4 Play durdurulup yeni sahne iÃ§in Unity kapatÄ±ldÄ±. HenÃ¼z yeni test/build sonucu yok.

## v0.5 uygulama ve ilk doÄŸrulama

Living sahnesi ve kaynak varlÄ±klar Ã¼retildi; Ã¶nceki sÃ¼rÃ¼mler korundu. EÄŸrisel Sparky, gerÃ§ek pamuk dokulu kÄ±vrÄ±mlÄ± oda tekstili, taranmÄ±ÅŸ sandalye, HDR yansÄ±ma/sÄ±cak gÃ¶lge, Ã¼Ã§ tur hedefi/isteÄŸe baÄŸlÄ± risk yÄ±ldÄ±zlarÄ± ve kayÄ±t tabanlÄ± efektler baÄŸlandÄ±. Ä°lk gerÃ§ek 6.3 sn render: 70.87542 m / 275 stil / zincir 2. Ä°lk koÅŸu 31 EditMode +30 PlayMode =61 geÃ§ti. DÃ¶rt seed Ã—gÃ¼venli/riskli rota Ã—120 sn testinde Ã§arpÄ±ÅŸma/havuz tÃ¼kenmesi yok. Yatak/sandalye kadrajÄ± ve poster ayrÄ±ntÄ±larÄ± son incelemede iyileÅŸtirildi; nihai test/render/APK kontrolÃ¼ takip ediyor. Fiziksel tablet Ã¶lÃ§Ã¼mÃ¼ ve insan kabulÃ¼ aÃ§Ä±k.

## v0.5 son yerel doÄŸrulama

Son PlayMode 31/31, EditMode 31/31:62/62. Model raporu 173 renderer /63 mesh /205.618 Ã¼Ã§gen (inaktif havuz dahil), sÄ±fÄ±r eksik referans/shader/dekor collider. Son Android paketi ZIP/ARM64/paket/API/v2 imzasÄ±yla doÄŸrulandÄ±. SHA256: d23bad3943464dc0d08103ce9daf2d887868a70fd58a5c5a05ee369427f64341. GerÃ§ek fiziksel cihaz ve insan kabulÃ¼ hÃ¢lÃ¢ aÃ§Ä±k. Kaynak/Ã¶n sÃ¼rÃ¼m/README ve Notion eÅŸitlemesi teslim adÄ±mÄ±dÄ±r.


## v0.5 yakÄ±n plan dÃ¼zeltmeleri

Mobilya/masa kesiÅŸmesi ve Ã¶rtÃ¼ye gÃ¶mÃ¼len yÄ±ldÄ±zlar gerÃ§ek ayrÄ±ntÄ± renderÄ±nda yakalanÄ±p dÃ¼zeltildi. FBX kÃ¶k Ã¶lÃ§ek/dÃ¶nÃ¼ÅŸ korunarak sandalye doÄŸru oda boyutuna alÄ±ndÄ±. Ãœreticide Â±6.3 masa aÃ§Ä±klÄ±ÄŸÄ± ve birleÅŸtirme sonrasÄ± sandalye Ã¶lÃ§Ã¼sÃ¼ kontrolÃ¼ var. Yeni testle son koÅŸu 31 EditMode +31 PlayMode =62/62 geÃ§ti. 87 kullanÄ±lmayan ara Living mesh kaldÄ±rÄ±ldÄ±; mevcut sahne/prefab baÄŸÄ±mlÄ±lÄ±klarÄ± korundu. Nihai APK yeniden derlendi ve v2 imza/paket/ZIP doÄŸrulandÄ±. Ä°lk ara APK yayÄ±mlanmadÄ±.

## v0.5 teslim eÅŸitlemesi kapandÄ±

Kaynak a7e889ae4264656eca81db093034c63ed15f3906, PR #4 ve v0.5.0 yayÄ±mlandÄ±. GitHub APK digest son yerel SHA256 ile aynÄ±: d23bad3943464dc0d08103ce9daf2d887868a70fd58a5c5a05ee369427f64341. Main README 0a91e43 ile gÃ¼ncellendi. Son APK tablet Download iÃ§inde 40,4 MB olarak doÄŸrulandÄ±; kurulum/oynanÄ±ÅŸ Ã¶lÃ§Ã¼mÃ¼ bekler. Notion kÃ¶kÃ¼ 27 Ã§ocuk sayfayla korundu, 25 plan/26 sonuÃ§/27 cihaz sayfalarÄ± ve ilgili 12 belge/kanÄ±t sayfasÄ± gÃ¼ncellendi; kÃ¶k/teslim/gÃ¶rsel/cihaz sayfalarÄ± yeniden okunarak doÄŸrulandÄ±. Unity ToyRoomLiving sahnesiyle yeniden aÃ§Ä±ldÄ±. Fiziksel FPS/Ä±sÄ±/dokunma ve insan tempo/ses/sanat kabulÃ¼ aÃ§Ä±k.

## v0.6 son Android paket doÄŸrulamasÄ±

38.232.631 bayt non-Development ARM64 IL2CPP APK Ã¼retildi. SHA256 65750071216c7742625eab7f8e410f027a815f96741d3518c6eecb6821ead042. ZIP/v2 imza, paket/API/portre, ZIP16KB ve altÄ± native ELF LOAD16KB geÃ§ti. Android Debug sertifikasÄ± test daÄŸÄ±tÄ±mÄ± iÃ§indir. Tek izin INTERNET; varsayÄ±lan endpoint boÅŸ/uygunluk kapalÄ±. allowBackup Ã¶zel olarak kapalÄ± deÄŸil; OEM/OS yedek davranÄ±ÅŸÄ± gizlilik taslaÄŸÄ±nda aÃ§Ä±klandÄ±. Cihaz/16KB gerÃ§ek ortam/maÄŸaza onayÄ± yok.

## v0.7 son doÄŸrulama ve teslim hazÄ±rlÄ±ÄŸÄ±

44 EditMode +43 PlayMode =87/87 geÃ§ti. Son 16 gerÃ§ek render incelendi; pitch/pause, rampa kapaÄŸÄ±, masa Ã§erÃ§evesi ve duvar/oda dÃ¼zeni dÃ¼zeltildi. Model denetimi 0 eksik mesh/materyal/shader/dekor collider; tÃ¼m inaktif araÃ§lar ve eski baÅŸlangÄ±Ã§ havuzu dahil 406 renderer/1.012.691 Ã¼Ã§gen/237 mesh (runtime gÃ¶rÃ¼nÃ¼r maliyet veya FPS deÄŸildir). Son Android paket v2/ZIP/ELF16KB ve sÃ¼rÃ¼m kontrolÃ¼nden geÃ§ti. Notion 33 sonuÃ§ sayfasÄ± oluÅŸturuldu. Film kalitesi, insan dinleme, yeni tablet kabulÃ¼ ve canlÄ± servis aÃ§Ä±k kalÄ±r.

## v0.7 yayÄ±n ve eÅŸitleme tamamlandÄ±

Kaynak 0a2cbf03fce7acdc796dfa9dd2cbfb56fab430e9, PR #6 ve v0.7.0 Ã¶n sÃ¼rÃ¼m GitHub Ã¼zerinde. APK GitHub digest/yerel SHA256 eÅŸleÅŸti. Main README 8fa898a ile gÃ¼ncel kaynak dalÄ±nÄ± gÃ¶sterir. Notion 32 plan/33 sonuÃ§, gÃ¼nlÃ¼k ve ilgili 12 sayfa gÃ¼ncellendi; root/sonuÃ§/gÃ¶rsel readback doÄŸrulandÄ± ve 33 alt sayfa korundu. GerÃ§ek tablet ve insan kalite kabulÃ¼ aÃ§Ä±k; film/maÄŸaza final onayÄ± diye sunulmaz.

## v0.8 son kabul koÅŸusu

48 EditMode +54 PlayMode =102/102 geÃ§ti. Son ekler: gerÃ§ek zemin/hava risk yÄ±ldÄ±zlarÄ± ve gÃ¶rev baÄŸlantÄ±sÄ±; tur bitiÅŸinde aktif gÃ¼Ã§/lift temizliÄŸi ve haklarÄ±n iade edilmemesi. Kenar/yan rampa, kapÄ± sinyali, domino aÃ§ma ve iki mod/Ã¼Ã§ seed gÃ¼venli tanÄ±k geÃ§ti. Garaj yakÄ±n planda far/arka kanat baÄŸlantÄ±larÄ± dÃ¼zeltilip yeni prefab/sahne varyantlarÄ±na kaydedildi. KullanÄ±cÄ± TP/araÃ§larÄ± ve Ã¶nceki sahneler korunur. Notion yeni sayfa blok limiti nedeniyle reddedildi; otomatik tekrar yok, eÅŸitleme bekleyen notlar yerel/GitHub'da.

## v0.8 yayÄ±n ve kaynak eÅŸitlemesi

Kaynak 0cda6024cfe585500df58cc61e3c232d7125bb35; PR #7 ve v0.8.0 Ã¶n sÃ¼rÃ¼m yayÄ±mlandÄ±. APK GitHub digest/yerel SHA256 aynÄ±. Main README c356447d1fcd48026b024fc45b854c6e9f7f7ea9 ile gÃ¼ncel, gerÃ§ek kaynak dalÄ± ayrÄ±mÄ± aÃ§Ä±k. Son 102/102 test, 29 gerÃ§ek gÃ¶rÃ¼ntÃ¼, model/Android raporlarÄ± kaynakta. Notion yeni sayfa blok limiti nedeniyle oluÅŸturulamadÄ±; baÅŸlangÄ±Ã§ gÃ¼nlÃ¼ÄŸÃ¼ Ã¶nceden doÄŸrulandÄ±, sonraki notlar GitHub/yerelde. Fiziksel ve insan kabulÃ¼ aÃ§Ä±k.

## v0.8.1 teslim doÄŸrulandÄ±

Kaynak af0c0e6bdacbc435f3252a6dd5498035deffe402, PR #8 ve v0.8.1 Ã¶n sÃ¼rÃ¼m GitHub Ã¼zerinde. APK digest yerel SHA256 ile eÅŸleÅŸti; main README bc370471f780b633d940a818dced2d94c9b6c741 ile gÃ¼ncel. 104/104 Unity, 9 gerÃ§ek gÃ¶rÃ¼ntÃ¼ ve model/Android denetimleri kaynakta. Notion eÅŸitlemesi Ã¶nceki Ã¼cretsiz blok sÄ±nÄ±rÄ± nedeniyle yerel notta bekler. Yeni fiziksel tablet kabulÃ¼ henÃ¼z yapÄ±lmadÄ±.
