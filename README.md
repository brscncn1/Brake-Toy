# GÃ¼ncel Ã§alÄ±ÅŸma â€” v0.10 oyuncu deneyimi adayÄ±

[Paket raporu](Docs/PLAYER_EXPERIENCE_PACKAGES-2026-10-27.md): dÃ¼zenlenen HUD/menÃ¼/ayarlar,
hafÄ±zalÄ± deÄŸiÅŸken ritimli parkur, Ã¼Ã§ araÃ§ gÃ¶vdesi ve geri alÄ±nabilir masa kaplamasÄ±.
187benzersiz Unity testi; [gerÃ§ek gÃ¶rÃ¼ntÃ¼ler](Reports/Visuals/EXPERIENCE_GALLERY.html).
Kaynak feat/player-experience-packages. Tablet ve sanat kullanÄ±cÄ± kabulÃ¼ bekliyor; K6 baÅŸlamadÄ±.
Android paketinin gerÃ§ek durumu Reports/EXPERIENCE_DELIVERY.json dosyasÄ±nda ayrÄ±ca belirtilecek.

# BRAKE TOY

26 Ekim [oda/rampa dÃ¼zeltmesi ve oyuncu incelemesi](Docs/FRESH_EYES_REVIEW-2026-10-26.md):
oda gece de aydÄ±nlÄ±k; dÄ±ÅŸ manzara/gece lambalarÄ± dÃ¶ngÃ¼de. Rampa Ã§Ä±kÄ±ÅŸlarÄ± aÃ§Ä±k/alÃ§ak engelli karÄ±ÅŸÄ±k.
Nesneler silinmedi.179/179 Unity testi ve [16gerÃ§ek kamera karesi](Reports/Visuals/CHOICES_REVIEW_GALLERY.html).
Kaynak dalÄ± fix/bright-room-ramp-choices; yeni APK Ã¼retilmedi. Ä°ncelemedeki diÄŸer Ã¶neriler uygulanmÄ±ÅŸ sayÄ±lmaz.

26 Ekim [top hareketi ve gece lambalarÄ±](Docs/ROLLING_AND_NIGHT-2026-10-26.md):
yerinde dÃ¶nÃ¼ÅŸ hatasÄ± dÃ¼zeltildi; dÃ¶rt hareket profili, masa Ã¼stÃ¼ yola baÄŸlÄ± yuvarlanma ve iki loÅŸ lamba.
174/174 Unity testi; [gerÃ§ek gÃ¶rÃ¼ntÃ¼ler](Reports/Visuals/ROLLING_NIGHT_GALLERY.html).
Kaynak dalÄ± fix/rolling-ball-motion; yeni APK henÃ¼z Ã¼retilmedi.

26 Ekim parkur dÃ¼zeltmesi: [kitap/defter/kalemlik ve otobÃ¼s geri dÃ¶ndÃ¼](Docs/TABLETOP_COURSE-2026-10-26.md).
Sekiz oyuncak ailesine Ã¼Ã§lÃ¼ geÃ§itler, kÄ±sa slalom, dar merkez ve S dizileri eklendi;
sÃ¼rekli aÃ§Ä±k yeÅŸil ÅŸerit kaldÄ±rÄ±ldÄ±, kitap rampalarÄ± korundu. 165/165 Unity testi geÃ§ti.
[GerÃ§ek motor gÃ¶rÃ¼ntÃ¼leri](Reports/Visuals/TABLETOP_GALLERY.html) Â· [doÄŸrulama](Reports/TABLETOP_COURSE_VALIDATION.json).
Kaynak dalÄ± feat/dense-tabletop-course; yeni APK Ã¼retilmedi. Ã–nceki deneyler aÅŸaÄŸÄ±da tarihseldir.

26 Ekim: [kurmalÄ± oyuncak/trende iki bitiriÅŸ](Docs/ENDING_VARIANTS-2026-10-26.md),
[klasik/yeni gÃ¶rÃ¼ntÃ¼ler](Reports/Visuals/ENDING_GALLERY.html). 159/159 regresyon ve5/5 son hedefli kontrol.
HÄ±z eÄŸrisi korundu; yeni APK henÃ¼z Ã¼retilmedi.

25 Ekim K2.1: [ilk dakika tempo Ã¶lÃ§Ã¼mÃ¼](Docs/OPENING_PACE-2026-10-25.md),
[10/30/60 saniye motor gÃ¶rÃ¼ntÃ¼leri](Reports/Visuals/OPENING_GALLERY.html).
149/149 Unity testi; hÄ±z/parkur kurallarÄ± deÄŸiÅŸmedi, yeni APK Ã¼retilmedi.

25 Ekim K1.2: [temiz GitHub klonu doÄŸrulandÄ±](Docs/CLEAN_CLONE-2026-10-25.md),
145/145 Unity testi; baÄŸÄ±msÄ±z C# 19/19 ve sunucu 9/9 ayrÄ±ca geÃ§ti. Yeni APK Ã¼retilmedi.

25 Ekim K1.6/K1.7: [aÃ§Ä±k Android profilleri](Docs/ANDROID_BUILD_PROFILES.md), 74/74 Editor testi.
Dal `fix/explicit-android-build-target`; APK Ã¼retmeden hedef doÄŸrulamasÄ± eklendi.

25 Ekim K1.5: [iniÅŸ ve rampa sÄ±nÄ±rlarÄ±](Docs/LANDING-2026-10-25.md), 124/124 Unity testi.
Dal `fix/landing-ramp-boundaries`; indirmedeki APK bu yeni dÃ¼zeltmeleri henÃ¼z iÃ§ermez.

25 Ekim saÄŸlamlaÅŸtÄ±rma: [Relax toparlanmasÄ± ve kayÄ±t geÃ§iÅŸi](Docs/HARDENING-2026-10-25.md),
53 EditMode +66 PlayMode =119/119. Dal `fix/adventure-relax-records`; yeni APK henÃ¼z Ã¼retilmedi.

25 Ekim dÃ¼zenlemesi: sÃ¼rÃ¼m/test/build kanÄ±tlarÄ± [Reports](Reports/README.md),
galeriler `Reports/Visuals`, eski plan ve kilometre taÅŸlarÄ±
[Docs/Archive/pre-v09](Docs/Archive/pre-v09/README.md) altÄ±nda tutulur.
Bu dÃ¼zenlemenin dalÄ± `chore/project-organization`; oyun sÃ¼rÃ¼mÃ¼ v0.9.0 olarak kalÄ±r.

**NO BRAKES. BIG TROUBLE.** Unity 6.3 LTS / C# / gerÃ§ek 3D URP / Android portre.

## Agent belgeleri â€” 24 Ekim 2026

Codex ve Claude Code iÃ§in ortak giriÅŸ [AGENTS.md](AGENTS.md); [yol haritasÄ±](YOL_HARITASI.md), [mimari](MIMARI.md), [mekanikler](MEKANIKLER.md). YardÄ±mcÄ±lar: [Claude](CLAUDE.md), [kararlar](KARARLAR.md), [doÄŸrulama](DOGRULAMA.md). Her dosya en fazla 300 satÄ±rdÄ±r; konuya gÃ¶re okunur. GÃ¼ncel belge dalÄ± **docs/agent-handbook**, oyun tabanÄ± v0.9 Adventure. Eski Ä°ngilizce/faz belgeleri tarihsel kayÄ±ttÄ±r. Notion merkez ve mevcut belge Ã¶zetleri/kaynak baÄŸlantÄ±larÄ± gÃ¼ncellendi; yeni tam sayfa kopyalarÄ± Ã¼cretsiz blok kotasÄ± nedeniyle bekliyor ([eÅŸitleme kapsamÄ±](Docs/NOTION_SYNC-agent-handbook.json)). Belge turunda oyun kodu/APK deÄŸiÅŸmedi, Unity testleri yeniden koÅŸulmadÄ±.

## v0.9 â€” Adventure

**Sekiz yeni parkur mekaniÄŸi:** iki Ã§Ä±kÄ±ÅŸlÄ± karton tÃ¼nel, kurmalÄ± kurbaÄŸa/Ã¶rdek, sallanan oyuncak vinÃ§, enine oyuncak tren, 1â€“3 misketli oluk, yapÄ±ÅŸkan bant/kaygan kartlar, cetvel tahterevalli ve kapÄ±sÄ±ndan araÃ§ Ã§Ä±kan oyuncak garaj.

Sekizli karÄ±ÅŸtÄ±rÄ±lmÄ±ÅŸ seÃ§im, son Ã¼Ã§ aileyi tekrarlamama, baÄŸlÄ± yaklaÅŸma/karar/atlayÄ±ÅŸ dizileri ve aÃ§Ä±k yeÅŸil yol. HÄ±z eÄŸrisi korunur; yÃ¼zey ve zamanlama kararlarÄ± yenidir. Tidy yerleÅŸimi, raf radyosu, mÃ¼zik ve TP/araÃ§/gÃ¼Ã§ envanteri korunur. Domino yoktur.

![GerÃ§ek Unity parkur gÃ¶rÃ¼nÃ¼mÃ¼](Reports/Visuals/Adventure-10.png)

[APK v0.9.0](https://github.com/brscncn1/Brake-Toy/releases/tag/v0.9.0) Â· [Kaynak feat/adventure-v09](https://github.com/brscncn1/Brake-Toy/tree/feat/adventure-v09) Â· [SonuÃ§/kanÄ±t](Reports/MILESTONE-v09.md) Â· [13 gerÃ§ek Unity gÃ¶rÃ¼ntÃ¼sÃ¼](Reports/Visuals/ADVENTURE_GALLERY.html)

Unity **6000.3.26f1** â†’ Tools â†’ Brake Toy â†’ **Open Adventure v0.9** â†’ Play. Game 9:16, 1Ã—. Paket com.braketoy.prototype, **0.9.0/kod10**, ARM64 IL2CPP non-Development; Ã¶ncekinin Ã¼zerine gÃ¼ncellenir. Kariyer aynÄ± dosyada, yeni parkurun rekor/ghost kayÄ±tlarÄ± ayrÄ±dÄ±r. **112/112 test: 50 EditMode +62 PlayMode.** Fiziksel tablet ve insan gÃ¶rsel/oynanÄ±ÅŸ kabulÃ¼ bekler. Notion yeni not eÅŸitlemesi Ã¼cretsiz blok kotasÄ± nedeniyle bekler; yerel/GitHub gÃ¼nlÃ¼kleri gÃ¼nceldir.

AÅŸaÄŸÄ±daki v0.8.1 ve Ã¶nceki bÃ¶lÃ¼mler tarihsel teslim kayÄ±tlarÄ±dÄ±r; gÃ¼ncel oyun Adventure sahnesidir.

## v0.8.1 â€” YerleÅŸim dÃ¼zeltmesi arÅŸivi

Mavi kenar dekorlarÄ± defter/kalemlikten ayrÄ±ldÄ±, radyo soldaki rafÄ±n Ã¼stÃ¼ne taÅŸÄ±ndÄ±, domino aktif oyundan kaldÄ±rÄ±ldÄ±. HÄ±z/zorluk ayarlarÄ± korunur. Ã–nceki kariyer/TP/araÃ§/gÃ¼Ã§ haklarÄ± sÃ¼rer.

![GerÃ§ek Unity yerleÅŸimi](Reports/Visuals/Tidy-6.png)

[APK v0.8.1](https://github.com/brscncn1/Brake-Toy/releases/tag/v0.8.1) Â· [v0.8.1 kaynak](https://github.com/brscncn1/Brake-Toy/tree/fix/table-layout-v081) Â· [104/104 test ve sonuÃ§](Docs/Archive/pre-v09/MILESTONE-v081.md)

Unity 6000.3.26f1 â†’ Tools â†’ Brake Toy â†’ **Open Tidy Room v0.8.1** â†’ Play. Paket com.braketoy.prototype, 0.8.1/kod9; APK Ã¶ncekinin Ã¼zerine gÃ¼ncellenir. Ã–nceki v0.8 aÃ§Ä±klamalarÄ± aÅŸaÄŸÄ±da tarihsel olarak korunur; bu sÃ¼rÃ¼mÃ¼n sahnesi Tidy'dir.


## v0.8 â€” Playground

GÃ¼venli/riskli yollar, baÄŸlÄ± karÅŸÄ±laÅŸmalar, kitap rampalarÄ±nÄ±n teker kenarÄ± dÃ¼zeltmesi, domino ile yol aÃ§ma ve sinyalli oyuncak kapÄ±sÄ±. 3 sn yavaÅŸlatma, korumalÄ± roket ve 5 sn jetpack; baÅŸlangÄ±Ã§ta Ã¼Ã§er hak, TP yenileme. Geri alÄ±nabilir masa bÃ¶lgeleri, Ä±ÅŸÄ±k/malzeme dÃ¼zenlemesi, kompakt menÃ¼, gÃ¶rsel gÃ¶rev kartlarÄ± ve ayrÄ± araÃ§ siluetleri. AyrÄ±ntÄ±lÄ± final model/sanat kabulÃ¼ bekler.

![GerÃ§ek Unity menÃ¼sÃ¼](Reports/Visuals/Playground-0.png)

[APK v0.8.0](https://github.com/brscncn1/Brake-Toy/releases/tag/v0.8.0) Â· [Kaynak](https://github.com/brscncn1/Brake-Toy/tree/feat/playground-v08) Â· [AyrÄ±ntÄ±lÄ± sonuÃ§/eleÅŸtiri](Docs/Archive/pre-v09/MILESTONE-v08.md)

## v0.8 arÅŸivi: AÃ§ ve oyna

1. feat/playground-v08 dalÄ±nÄ± Unity **6000.3.26f1** ile aÃ§. Tools â†’ Brake Toy â†’ **Open Playground v0.8** â†’ Play. Game 9:16, 1Ã—.
2. SÃ¼rÃ¼kle; yeÅŸil rota gÃ¼venli, altÄ±n rota isteÄŸe baÄŸlÄ± risk/Ã¶dÃ¼l. Kitap rampasÄ±ndaki yeÅŸil yaklaÅŸma oku atlayÄ±ÅŸÄ± gÃ¶sterir. Havada kazanÄ±lan TP bÃ¶lÃ¼mÃ¼ 2x.
3. ÃœÃ§ gÃ¼Ã§ dÃ¼ÄŸmesi sÃ¼rÃ¼ÅŸte; aynÄ± anda bir gÃ¼Ã§. Haklar kalÄ±cÄ±dÄ±r, retry yenilemez. MenÃ¼ â†’ GÃœÃ‡ HAKLARI: 350/500/650 TP. GerÃ§ek para/coin yok.
4. Ayarlar â†’ MASA: KLASÄ°K / DOMÄ°NO: KAPALI; iki deney ayrÄ± geri alÄ±nabilir. Eski v0.7 sahnesi ayrÄ±ca korunur.
5. APK Ã¶ncekinin Ã¼zerine gÃ¼ncellenir: com.braketoy.prototype, 0.8.0/kod8, ARM64 IL2CPP non-Development test paketi; maÄŸaza imzasÄ± deÄŸildir. TP/aÃ§Ä±k araÃ§lar taÅŸÄ±nÄ±r, yeni rekor/kariyer/ghost ayrÄ±dÄ±r. GÃ¼Ã§ kullanÄ±lan turda ghost yazÄ±lmaz.

## v0.8 arÅŸivi: KanÄ±t ve belgeler

**102/102 Unity testi** (48 EditMode +54 PlayMode).

[GerÃ§ek gÃ¶rÃ¼ntÃ¼ler](Reports/Visuals/PLAYGROUND_GALLERY.html), [plan](Docs/Archive/pre-v09/PLAN-v0.8.md), [model agent devir notu](MODEL_HANDOFF-v08.md), [tasarÄ±m](Docs/Archive/pre-v09/GAME_DESIGN.md), [mimari](Docs/Archive/pre-v09/ARCHITECTURE.md), [gÃ¼nlÃ¼k](DEV_LOG.md). ANDROID_BUILD_REPORT-v08.json, UNITY_TEST_RESULTS-v08.json ve MODEL_VALIDATION-v08.json.

Fiziksel v0.8 FPS/p95/Ä±sÄ±nma ve insan kalite kabulÃ¼ bekler. CanlÄ± online kapalÄ±; yeni kurallar eski referans sunucuya gÃ¶nderilmez. Notion mevcut proje merkezi korunur, Ã¼cretsiz blok sÄ±nÄ±rÄ± yeni sayfayÄ± reddetti: [eÅŸitleme durumu](NOTION_PENDING-v08.md). MÃ¼zik ve Ã¶nceki sÃ¼rÃ¼m kanÄ±tlarÄ± korunur.
