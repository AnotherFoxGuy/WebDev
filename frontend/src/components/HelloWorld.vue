<template>
    <div class="group realtimedb">
        <h2>Realtime Datastore</h2>
        <div class="input-group half left">
            <input type="text" v-model="value" @input="handleFNameUpdate()"/>
        </div>
    </div>
</template>

<script>
    export default {
        name: "HelloWorld",
        props: ['ds'],
        data() {
            return {
                value: '',
            };
        },
        created() {
            this.record = this.ds.record.getRecord('Rules');

            this.record.subscribe(values => {
                this.value = values.rules;
            })
        },
        methods: {
            handleFNameUpdate: function () {
                this.record.set('rules', this.value);
            }
        }
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
    h3 {
        margin: 40px 0 0;
    }

    ul {
        list-style-type: none;
        padding: 0;
    }

    li {
        display: inline-block;
        margin: 0 10px;
    }

    a {
        color: #42b983;
    }
</style>
