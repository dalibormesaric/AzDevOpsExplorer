import React, { useEffect, useState } from 'react'
import { Link, useParams } from 'react-router-dom'

type Release = {
    CreatedOn: Date
    Name: string
    Id: number
}

export const ReleaseList = () => {
    let { project } = useParams<{ project: string }>()
    const [releaseList, setReleaseList] = useState<Release[]>()
    const [isLoading, setIsLoading] = useState(true)

    const getData = async () => {
        let response = await fetch(`http://localhost:8080/releases/${project}`)
        setReleaseList(await response.json())
        setIsLoading(false)
    }

    useEffect(() => {
        setIsLoading(true)
        getData()
        // eslint-disable-next-line
    }, [])

    return (    
        <div>
            <h1><Link to="/">Projects</Link> / Releases</h1>
            <div className="py-8">
                {!isLoading && releaseList &&
                    <table className="min-w-full">
                        <tbody className="bg-white divide-y divide-gray-200">
                            {releaseList && releaseList.map(f => (
                                <tr key={f.Name}>
                                    <td><Link to={`/releases/${project}/${f.Id}`}>{f.Name}</Link></td>
                                    <td>{new Date(f.CreatedOn).toDateString()}</td>
                                </tr>
                            ))}
                        </tbody>
                </table>}
                {!isLoading && (!releaseList || (releaseList && releaseList.length === 0)) &&
                    <span>No data</span>}
                {isLoading && 
                <span>Loading...</span>}
            </div>
        </div>
    )
}