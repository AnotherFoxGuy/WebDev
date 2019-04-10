<template>
    <div class="box">
        <h4>
            <font-awesome-icon icon="scroll"/>
            Rules
        </h4>
        <p class="content">
            {{data.Rules}}
        </p>
    </div>
</template>

<script>
    export default {
        name: "Rules",
        props: ['ds'],
        data() {
            return {
                data: {
                    Rules: "?"
                }
            }
        },
        created() {
            this.record = this.ds.record.getRecord('Rules');

            this.record.subscribe(val => {
                console.log(val);
                this.data = val;
            })
        }
    }
</script>

<style scoped>
    /*.box {
        overflow: hidden;
        position: relative;
    }

    .content {
        z-index: -10;
        position: absolute;
        margin: 0;
        text-align: center;
        transform: translateY(100%);
        animation: scroll-up 30s linear infinite;
    }

    @keyframes scroll-up {
        0% {
            transform: translateY(60%);
        }
        100% {
            transform: translateY(-105%);
        }
    }*/
</style>
