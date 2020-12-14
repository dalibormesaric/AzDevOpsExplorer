type CardProps = { title: string }
export const Card = ({ title }: CardProps) => {
    return (    
        <div className="shadow-xl bg-white rounded-lg h-18 hover:shadow-2xl">
            <div className="p-8">
                {title}
            </div>
        </div>
    )
};