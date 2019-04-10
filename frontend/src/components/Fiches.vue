<template>
    <div class="box">
        <h4>
            <font-awesome-icon icon="scroll"/>
            Fiches
        </h4>
        <div class="FichesContainer"
             v-for="(item, index) in chips">
            <div>
                <img :src="'/images/' + index + '.png'"/>
                {{index}}
                <div>
                    {{ item }}
                </div>
            </div>
        </div>
        <hr/>
        <div>
            <p>Rebuy: {{data.Rebuy}}</p>
        </div>
        <div>
            <p>Starting budget: {{data.Starting_Budget}}</p>
        </div>
    </div>
</template>

<script>
    export default {
        name: "Fiches",
        props: ['ds'],
        data() {
            return {
                data: {},
                chips: {}
            }
        },
        created() {
            this.record = this.ds.record.getRecord('Chips');

            this.record.subscribe(val => {
                console.log(val);
                this.data = val;
                this.chips = JSON.parse(val.Chips)
            })
        }
    }
</script>

<style scoped lang="scss">
    @import '~foundation-sites/scss/util/util';

    .FichesContainer {
        display: inline-block;
        width: 55px;
        height: auto;
        text-align: center;
    }

    img {
        text-align: center;
        display: block;
        max-width: 80%;
        height: auto;
    }
</style>