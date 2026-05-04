<script setup lang="ts">
const props = defineProps({
  class: {
    type: String,
  },
  layoutClass: {
    type: String,
  },
})
</script>

<template>
  <div class="slidev-layout two-cols-header-with-footer w-full h-full" :class="layoutClass">
    <div class="col-header">
      <slot />
    </div>
    <div class="col-left" :class="props.class">
      <slot name="left" />
    </div>
    <div class="col-right" :class="props.class">
      <slot name="right" />
    </div>
    <div class="col-bottom" :class="props.class">
      <slot name="bottom" />
    </div>

    <div class="w-11/12 fixed bottom-0 flex justify-between mb-2 text-xs footer">
      <span>{{ $slidev.configs.occasion }}</span>
      <span>{{ $slidev.configs.presenter }} - {{ $slidev.configs.contact }}</span>
      <span style="min-width: 4rem;"><SlideCurrentNo /> / <SlidesTotal /></span>
    </div>
  </div>
</template>

<style scoped>
.two-cols-header-with-footer {
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  grid-template-rows: auto 1fr auto;
  column-gap: 15px;
}

.col-header {
  grid-area: 1 / 1 / 2 / 3;
}
.col-left {
  grid-area: 2 / 1 / 3 / 2;
}
.col-right {
  grid-area: 2 / 2 / 3 / 3;
}
.col-bottom {
  align-self: end;
  grid-area: 3 / 1 / 3 / 3;
}
</style>
