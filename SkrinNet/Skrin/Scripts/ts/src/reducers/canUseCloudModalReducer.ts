import { combineReducers } from 'redux';

import {SHOW_CAN_USE_CLOUD_MODAL,HIDE_CAN_USE_CLOUD_MODAL} from '../models/Constants'
import {IAction} from '../models/Common'
import {IActionCanUseCloudDialog} from '../actions/AccessCloudActionCreators'

const isShowing=(state:boolean=false,action:IAction)=>{
    switch (action.type) {
        case SHOW_CAN_USE_CLOUD_MODAL:    
            return state=true;
         case HIDE_CAN_USE_CLOUD_MODAL:
            return state=false;
        default:
            return state;
    }
}

const action=(state:any=null,action:IActionCanUseCloudDialog)=>{
    switch (action.type) {
        case SHOW_CAN_USE_CLOUD_MODAL:    
            return state=action.action;
        default:
            return state;
    }
}

export default combineReducers({
    isShowing:isShowing,
    action:action
})