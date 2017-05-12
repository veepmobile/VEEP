import * as React from "react";

interface IProgressBarProps {
    upload_progress:number | null;
};

interface IProgressBarState {};

class ProgressBar extends React.Component<IProgressBarProps, IProgressBarState> {

    

    public render(): JSX.Element {

        if(this.props.upload_progress===null){
            return(<span></span>);
        }



        let shown_progress=this.props.upload_progress==null ? "" : this.props.upload_progress+"%";

        return (
            <div className="progress">
                <div className="progress-bar"  style={{width:this.props.upload_progress+"%"}}>
                    {this.props.upload_progress+"%"}
                </div>
            </div>
        );
    }
}

export default ProgressBar;
