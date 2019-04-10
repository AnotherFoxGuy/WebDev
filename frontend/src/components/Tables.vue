<template>
    <div id="TableBukket">
        <div class="info-box">
            <div><font-awesome-icon icon="hourglass"/> {{timer}} </div>
            <div><font-awesome-icon icon="circle-notch"/> {{data.Round}}/{{data.TotalNumberOfRounds}}</div>
            <div v-if="data.Pause === true"><font-awesome-icon icon="pause"/> Pause </div>
        </div>

        <hr>
        <div class="grid-x tables-box grid-margin-x">
            <table class="cell small-12 large-3" v-for="(mainItem, index) in tables">
                <thead>
                    <tr>
                        <th colspan="2">{{index}}</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(childitem) in mainItem" v-if="childitem.Join_Game">
                        <td>{{childitem.Firstname}}</td>
                        <td>{{childitem.Lastname}}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script>
    import moment from 'moment';
    import momentDurationFormatSetup from "moment-duration-format";

    momentDurationFormatSetup(moment);

    export default {
        name: "Tables",
        props: ['ds'],

        data() {
            return {
                timer: "Waiting for data",
                data: {},
                tables: {}
            }
        },
        created() {
            this.record = this.ds.record.getRecord('Tables');
            this.GlobalRecord = this.ds.record.getRecord('Global');

            this.GlobalRecord.subscribe(val => {
                this.data = val;
            });

            this.record.subscribe(val => {
				console.log(val);
                this.tables = JSON.parse(val.Tables);
            });
            this.event = this.ds.event;
            this.event.subscribe('TimerUpdate', val => {
                this.timer = moment.duration(val).format();
            });
        }
    }
</script>

<style lang="scss" scoped>
    @import '~foundation-sites/scss/util/util';
	
	.alert{
		color: red;
		text-decoration: line-through;
	}

    .info-box{
        padding: 0.5em;
    }

    #TableBukket{
        width: 100%;
        height: 100%;
        overflow: hidden;
    }

    .tables-box {
        width: 100%;
        height: 100%;
        overflow: hidden;

        @include breakpoint(medium down) {
            font-size: small;
        }
    }
    table {
        table-layout: fixed;
    }

    td, tr {
        overflow: hidden;
        white-space: nowrap;
    }

    th, td{
        @include breakpoint(medium down) {
            padding: 0 !important;
        }
    }

</style>