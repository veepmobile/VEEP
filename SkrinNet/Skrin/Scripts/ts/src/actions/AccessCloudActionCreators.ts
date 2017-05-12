import {REQUEST_CAN_USE_CLOUD,RECEIVE_CAN_USE_CLOUD,SHOW_CAN_USE_CLOUD_MODAL,HIDE_CAN_USE_CLOUD_MODAL} from '../models/Constants'
import {IAction} from '../models/Common'
import AccessCloudAPI from '../api/AccessCloudAPI'
import {ModalActionCreators} from '../actions/ModalActionCreators'

export interface IActionRecieveCanUseCloud extends IAction{
    can_use_cloud:boolean
}

export interface IActionCanUseCloudDialog extends IAction{
    action:any;
}

export class AccessCloudActionCreators
{
    private static _requestCanUseCloud():IAction{
        return {type:REQUEST_CAN_USE_CLOUD};
    }

    private static _recieveCanUseCloud(can_use_cloud:boolean):IActionRecieveCanUseCloud{
        return{type:RECEIVE_CAN_USE_CLOUD,can_use_cloud:can_use_cloud}
    }

    static CanUseCloud(){
        return async (dispatch:any)=>{
            try
            {
                dispatch(this._requestCanUseCloud());
                let can_use_cloud = await AccessCloudAPI.CanUseCloud();
                dispatch(this._recieveCanUseCloud(can_use_cloud));
            }
            catch (error){
                 dispatch(ModalActionCreators.showModal("Ошибка запроса доступа к облаку: " + (error.message || error)));
            }
        }
    }

    static ConfirmCanUseCloud(action:any){
        return async (dispatch:any)=>{
            try{
                let can_use_cloud = await AccessCloudAPI.ConfirmUseCloud();
                if(can_use_cloud){
                    dispatch(action);
                    dispatch(this._recieveCanUseCloud(true));
                }
                dispatch(this.hideModal());
            }
             catch (error){
                 dispatch(this.hideModal());
                 dispatch(ModalActionCreators.showModal("Ошибка запроса доступа к облаку: " + (error.message || error)));
            }
        }
    }

    static hideModal():IAction{
        return{
            type:HIDE_CAN_USE_CLOUD_MODAL
        }
    }

    static showModal(action:any):IActionCanUseCloudDialog{
        return{
            type:SHOW_CAN_USE_CLOUD_MODAL,
            action:action
        }
    }
}