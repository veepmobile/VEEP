import { combineReducers } from 'redux';

import {SHOW_MODAL,SHOW_CONFIRM_MODAL,HIDE_MODAL} from '../models/Constants'
import {IAction} from '../models/Common'
import {IActionConfirmDialog} from '../actions/ModalActionCreators'

const is_showing=(state:boolean=false,action:IAction)=>{
    switch (action.type) {
        case SHOW_CONFIRM_MODAL:    
        case SHOW_MODAL:
            return state=true;
         case HIDE_MODAL:
            return state=false;
        default:
            return state;
    }
}

const body=(state:string='',action:IActionConfirmDialog)=>{
    switch (action.type) {
        case SHOW_CONFIRM_MODAL: 
        case SHOW_MODAL:   
            return state=action.body;        
        default:
            return state;
    }
}

const action=(state:any=null,action:IActionConfirmDialog)=>{
    switch (action.type) {
        case SHOW_CONFIRM_MODAL:    
            return state=action.action;
        case SHOW_MODAL:
            return state=null;
        default:
            return state;
    }
}

export default combineReducers({
    isShowing:is_showing,
    body:body,
    action:action
})