import * as React from "react";
import {IUserFile} from '../../models/IUserFile'
import {IDispatchGet,IFunction,IDispatch} from '../../models/Common'
import {GetStyleExtention,FormatBytes} from '../../lib/Utilites'
import ProgressBar from '../ProgressBar/ProgressBar'

interface IDispatchUploadFile{
    (file:File, issuer_id:string,can_use_cloud:boolean):void;
}

interface IIssuerUserFilesProps {
    files:IUserFile[],
    can_use_cloud:boolean,
    upload_progress:number | null,
    getFiles:IDispatchGet,
    uploadFile:IDispatchUploadFile,
    deleteFile:IFunction,
    issuer_id:string,
    file_size_limit:number,
    getFileSizeLimit:IDispatch,
    showErrorMessage:IFunction
};

interface IIssuerUserFilesState {};

class IssuerUserFiles extends React.Component<IIssuerUserFilesProps, IIssuerUserFilesState> {

  

    componentDidMount() {     
        this.props.getFiles(this.props.issuer_id);
        this.props.getFileSizeLimit();
    }


    loadfile(event:any){
        //console.log(event.target.files[0]);
        let file:File = event.target.files[0] as File;
        if (this.getFileSizes() + file.size > this.props.file_size_limit) {
                this.props.showErrorMessage("Превышен общий размер разрешенной загрузки: "+ FormatBytes(this.props.file_size_limit));
        }else{
            this.props.uploadFile(file,this.props.issuer_id,this.props.can_use_cloud); 
        }
        
    }

    private  getFileSizes(){
        let already_have=0;
        for(let i=0;i<this.props.files.length;i++){
            already_have+=this.props.files[i].file_size;
        }
        return already_have;
    }

    private CutFileName(file_name:string): JSX.Element{
        if(file_name.length<25){
            return <span className="file_name">{file_name}</span>;
        }
        return <span className="file_name title">
                {file_name.substring(0,25)}
                    <em>{file_name}</em>
                <span className="file_name_fader"></span>
            </span>;
    }


    public render(): JSX.Element {

        let files=this.props.files.map((file)=>{

            let extention=file.file_name.substr(file.file_name.lastIndexOf('.')+1,file.file_name.length);

           return(<tr key={file.id} className="file_item">
            <td>
                <div>
                    <a  href={"/UserFile/Load/"+file.id}>
                        <span className={GetStyleExtention(extention) + " ico"}></span>
                        {this.CutFileName(file.file_name)}
                    </a>
                    <span className="explain">  ({FormatBytes(file.file_size)})</span>
                    <span className="ddel" onClick={this.props.deleteFile.bind(null,file.id,file.file_name)}>
                        <span className="ddel_1">
                            <b>x</b>
                                Удалить
                            </span>
                        </span>
                    <div className="link_block_info">
                        <span className="explain">Дата загрузки:{file.update_date}</span>
                    </div>                    
                </div>
            </td>
            
            </tr>)
        });

        let upload_style=this.props.upload_progress==null ? {}:{display:"none"}
        
        return (            
            <div className="link_block" id="lb_1">                    
                    <h3 className="cloud_switcher_header">
                        <span className="icon-docs"></span>Файлы
                        <span className="icon-angle-down cloud_switcher_ar"></span>
                    </h3>
                    <table className="extra_table" style={{border:0}} cellPadding="0" cellSpacing="0">
                        <tbody>
                            <tr>
                                <td>
                                    <div className="icon_block large" style={upload_style}>
                                        <span className="icon-upload new ico"></span>
                                        <span>Загрузить</span>                                       
                                        <form>
                                            <input type="file" onChange={this.loadfile.bind(this)} name="cloud_file_input"/>
                                        </form>
                                    </div>
                                     <ProgressBar upload_progress={this.props.upload_progress} />                                   
                                </td>
                            </tr>
                             {files}
                        </tbody> 
                    </table>                   
                </div>);
    }
}

export default IssuerUserFiles;

