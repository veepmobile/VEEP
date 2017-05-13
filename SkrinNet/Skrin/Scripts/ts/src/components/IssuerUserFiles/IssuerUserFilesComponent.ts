import * as React from "react";
import { connect } from 'react-redux';



import  IssuerUserFiles  from "./IssuerUserFilesView";
import {IIssuerState} from "../../models/IIssuerState";
import {IssuerFileActionCreators} from '../../actions/IssuerFileActionCreators'
import {ModalActionCreators} from '../../actions/ModalActionCreators'
import {AccessCloudActionCreators} from '../../actions/AccessCloudActionCreators'



const mapStateToProps=(state:IIssuerState)=>(
    {
        files:state.files,
        upload_progress:state.upload_progress,
        issuer_id:state.issuer_id,
        file_size_limit:state.file_size_limit,
        can_use_cloud:state.can_use_cloud
    }
);

const mapDispatchToProps=(dispatch:any)=>(
    {
            getFiles:(issuer_id: string )=>(dispatch(IssuerFileActionCreators.GetFiles(issuer_id))),
            uploadFile:(file:File, issuer_id:string,can_use_cloud:boolean)=>{
                if(can_use_cloud){
                    dispatch(IssuerFileActionCreators.UploadFile(file,issuer_id));
                }else{
                    dispatch(AccessCloudActionCreators.showModal(IssuerFileActionCreators.UploadFile(file,issuer_id)));
                }  
            },
            deleteFile:(file_id:string,file_name:string)=>{
                dispatch(ModalActionCreators.showConfirmModal("Вы действительно хотите удалить файл: "+file_name +"?",IssuerFileActionCreators.DeleteFile(file_id)))                
            },
            getFileSizeLimit:()=>(dispatch(IssuerFileActionCreators.GetFileSizeLimit())),
            showErrorMessage:(error_message:string)=>(dispatch(ModalActionCreators.showModal(error_message)))
    }
);

export default connect(mapStateToProps,mapDispatchToProps)(IssuerUserFiles)
