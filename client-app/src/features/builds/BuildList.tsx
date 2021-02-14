import React, { useEffect, useState } from 'react'
import { Link, useParams } from 'react-router-dom'

type Build = {
    FinishTime: Date
    BuildNumber: string
    Id: number
}

export const BuildList = () => {
    let { project } = useParams<{ project: string }>()
    const [buildList, setBuildList] = useState<Build[]>()
    const [isLoading, setIsLoading] = useState(true)

    const getData = async () => {
        let response = await fetch(`http://localhost:8080/builds/${project}`)
        setBuildList(await response.json())
        setIsLoading(false)
    }

    useEffect(() => {
        setIsLoading(true)
        getData()
        // eslint-disable-next-line
    }, [])

    return (    
        <div>
            <h1><Link to="/">Projects</Link> / Builds</h1>
            <div className="py-8">
                {!isLoading && buildList &&
                    <table className="min-w-full">
                        <tbody className="bg-white divide-y divide-gray-200">
                            {buildList && buildList.map(f => (
                                <tr key={f.BuildNumber}>
                                    <td><Link to={`/builds/${project}/${f.Id}`}>{f.BuildNumber}</Link></td>
                                    <td>{new Date(f.FinishTime).toDateString()}</td>
                                </tr>
                            ))}
                        </tbody>
                </table>}
                {!isLoading && (!buildList || (buildList && buildList.length === 0)) &&
                    <span>No data</span>}
                {isLoading && 
                <span>Loading...</span>}
            </div>
        </div>
    )
}