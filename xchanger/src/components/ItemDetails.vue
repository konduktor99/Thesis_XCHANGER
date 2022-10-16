<template>

<div class ="main-item">

    <div class="slideshow" >
      <img name="slide"  style="width: 100%;"/>
      <a class="btn-prev"  onclick="changeImgBackwards()">&#10094;</a>
      <a class="btn-next" onclick="changeImgForward()">&#10095;</a>
    </div>

    <div class="details-item" >
      <h3>Kask Bell</h3>
      <h4>_konduktor_</h4>
      <h5>518557138</h5>
      <a href="">Więcej od tego ogłoszeniodawcy <i class="fa fa-chevron-right" ></i></a><br/><br/>
      <button type="button" class="btn btn-success btn-block">Zaproponuj wymianę <i class="fa fa-exchange" aria-hidden="true"></i></button>

    </div>

    <div class="description-item" >
      <h4>Opis</h4>
      <a >Kask Bell Super 3r. Odpinana szczęka. System ochrony MIPS, chroniący mózg przy uderzeniu. Kask mało używany, praktycznie jak nowy. Dla zdecydowanego kupującego możliwy dowóz na terenie Wałbrzycha i okolic. W razie pytań proszę dzwonić. Zapraszam.
        Platforma do wymiany wszelkiego rodzaju przedmiotów działająca zgodnie z postawą "anti-waste", sprzyjająca rozwojowi gospodarki o obiegu zamkniętym, gdzię jak najmniej artykułów codziennego użytku kończy przedwcześnie swój żywot na wysypisku lub zapomniane, na strychu. Dzięki naszemu portalowi sprawisz, że twoje rzeczy trafią do nowego właściciela, bez żadnych pośredników, a co za tym idzie bez generowania zbędnych kosztów dla Ciebie oraz planety, przy jednoczesnym zapewnieniu, że otrzymasz w zamian to czego potrzebujesz.
      </a>

    </div>
</div>
</template>

<script>


export default {
  name: 'ItemDetails',
   props: {
    title: String,
    desc: String,
    ownerNickname:String,
    ownerPhoneNum:String,
    
  }
}
</script>


