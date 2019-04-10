<template>
    <div id="app">

        <div class="overlay-box" v-if="data.GameStatus === 'Paused'">
            <div class="center">
                <font-awesome-icon icon="pause"/>
                Pause <br>
                <font-awesome-icon icon="hourglass"/> {{pauseTimer}}
            </div>
        </div>
		
		<div class="overlay-box" v-if="data.GameStatus === 'Setup'">
            <div class="center">
                <font-awesome-icon icon="hourglass"/>
                De volgende ronde begint over:
                 {{nextRoundTimer}}
            </div>
        </div>

        <div class="overlay-box" v-if="data.GameStatus === 'Stopped'">
            <div class="center">
                <font-awesome-icon icon="exclamation-triangle"/>
                No game running!
            </div>
        </div>

        <div id="scroll-box" v-if="draggable === false">
        </div>
        <grid-layout
                :layout.sync="layout"
                :is-draggable="draggable"
                @layout-updated="layoutUpdatedEvent">

            <grid-item v-for="item in layout"
                       :x="item.x"
                       :y="item.y"
                       :w="item.w"
                       :h="item.h"
                       :i="item.i">
                <Hands v-if="item.comp === 'Hands'" :ds="ds"/>
                <Fiches v-if="item.comp === 'Fiches'" :ds="ds"/>
                <Rules v-if="item.comp === 'Rules'" :ds="ds"/>
                <Tables v-if="item.comp === 'Tables'" :ds="ds"/>
            </grid-item>
        </grid-layout>

        <!--<HelloWorld :ds="ds" />-->

        <div id="draggable-button" class="button-group">
            <button v-on:click="draggable = !draggable" class="button"
                    v-bind:class="{ success: !draggable, alert: draggable }">
                Lock layout
            </button>

            <button v-on:click="resetLayout" class="alert button">
                Reset layout
            </button>
        </div>
    </div>
</template>

<script>
    import deepstream from 'deepstream.io-client-js';
    import Hands from "./components/Hands";
    import Rules from "./components/Rules";
    import Fiches from "./components/Fiches";
    import Tables from "./components/Tables";
    import VueGridLayout from 'vue-grid-layout';
    import HelloWorld from "./components/HelloWorld";
    import moment from 'moment';
    import momentDurationFormatSetup from "moment-duration-format";

    momentDurationFormatSetup(moment);


    const org_layout = [
        {x: 0, y: 0, w: 3, h: 2, i: "1", comp: "Hands"},
        {x: 0, y: 4, w: 3, h: 2, i: "2", comp: "Fiches"},
        {x: 0, y: 8, w: 3, h: 2, i: "3", comp: "Rules"},
        {x: 3, y: 0, w: 9, h: 6, i: "4", comp: "Tables"},
    ];

    export default {
        name: "app",
        components: {
            HelloWorld,
            GridLayout: VueGridLayout.GridLayout,
            GridItem: VueGridLayout.GridItem,
            Tables,
            Fiches,
            Rules,
            Hands,
        },
        data() {
            return {
                layout: [],
                draggable: false,
                ds: null,
                data: {},
                pauseTimer: "",
                nextRoundTimer: ""
            }
        },
        created() {
            this.layout = org_layout;
            this.ds = deepstream('anotherfoxguy.com:8181').login().on('connectionStateChanged', connectionState => {
                console.log(connectionState);
            });

            this.GlobalRecord = this.ds.record.getRecord('Global');

            this.GlobalRecord.subscribe(val => {
                console.log(val);
                this.data = val;
            });

            this.timerEvent = this.ds.event;
            this.timerEvent.subscribe('TimerUpdate', val => {
				if(this.data.GameStatus !== 'Running')
				{
					let ptime = moment.duration(this.data.Pause_Time);
					ptime.subtract(moment.duration(val));
					this.pauseTimer = ptime.format();
					
					let nrtime = moment.duration(1, 'm');
					nrtime.subtract(moment.duration(val));
					this.nextRoundTimer = nrtime.format();
				}
			
               
				
            });

        },
        mounted() {
            if (localStorage.getItem("layout") != null) {
                this.layout = JSON.parse(localStorage.getItem("layout"));
            }
        },
        methods: {
            layoutUpdatedEvent: function (newLayout) {
                localStorage.setItem("layout", JSON.stringify(newLayout));
            },

            resetLayout: function () {
                console.log(org_layout);
                this.layout = org_layout;
                localStorage.removeItem("layout");
            }
        }
    };
</script>

<style lang="scss">
    @import '~foundation-sites/scss/foundation.scss';

    @include foundation-everything();

    .overlay-box {
        width: 100%;
        height: 100%;
        z-index: 500;
        position: fixed;
        background-color: aliceblue;
    }

    .center {

        @include breakpoint(medium up) {
            font-size: 8rem;
        }

        @include breakpoint(medium down) {
            font-size: 4rem;
        }

        text-align: center;
        display: block;
        margin-left: auto;
        margin-right: auto;
        position: relative;
        top: 50%;
        transform: translateY(-50%);
    }

    #app {
        @include breakpoint(medium down) {
            font-size: small !important;
        }
    }

    #draggable-button {
        padding: 10px;
        z-index: 100;
        position: absolute;
    }

    #scroll-box {
        margin: -5em;
        height: 100000em;
        width: 100000em;
        z-index: 50;
        position: fixed;
        overflow: hidden;
    }

    h4 {
        background-color: #293c4c;
        color: aliceblue;
        padding: 5px;
        margin-top: 0;
    }

    .vue-grid-item {
        border: 2px solid grey;
    }

    .box {
        height: 100%;
        width: 100%;
        overflow: hidden;
    }

</style>
