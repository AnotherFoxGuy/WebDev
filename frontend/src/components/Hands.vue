<template>
    <div class="box">
        <h4>
            <font-awesome-icon icon="coins"/>
            Hands
        </h4>

    </div>
</template>

<script>

    export default {
        name: "Hands",
        props: ['ds'],
        data() {
            return {
                data: null
            };
        },
        created: function () {
            this.record = this.ds.record.getRecord('Hands');

            this.record.subscribe(val => {
                console.log(val);
                this.data = val;
            })
        }
    };
</script>

<style scoped>
</style>
