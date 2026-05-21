<template>
  <div class="slidev-layout cover">
    <div class="flex flex-col justify-between items-start ml-5">

      <div class="flex flex-col justify-center items-start ml-5 mt-10">
        <slot />
      </div>

      <div class="w-full flex justify-center">
        <img :src="$slidev.configs.occasionLogoUrl" alt="Occasion Logo" class="w-50 object-fit-contain" />
      </div>
    </div>
  </div>
</template>
