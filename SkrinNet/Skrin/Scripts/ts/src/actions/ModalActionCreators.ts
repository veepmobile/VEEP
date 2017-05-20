import {SHOW_MODAL,HIDE_MODAL,SHOW_CONFIRM_MODAL} from '../models/Constants'

import {IAction} from '../models/Common'


export interface IActionConfirmDialog extends IAction {
    body:string;
    action:any;
}

export  class ModalActionCreators{
    
    static showConfirmModal(body:string,action:any):IActionConfirmDialog{
        return{
            type:SHOW_CONFIRM_MODAL,
            body:body,
            action:action
        }
    }

    static showModal(body:string):IActionConfirmDialog{
        return{
            type:SHOW_MODAL,
            body:body,
            action:null
        }
    }

    static hideModal():IAction{
        return{
            type:HIDE_MODAL
        }
    }


}