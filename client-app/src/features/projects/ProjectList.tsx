import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import { Card } from '../components/Card'

type Project = {
    Name: string
}

export const ProjectsList = () => {
    const [projectList, setProjectList] = useState<Project[]>()

    const getData = async () => {
        let response = await fetch('http://localhost:8080/projects')
        setProjectList(await response.json())
    }

    useEffect(() => {
        getData()
    }, [])

    return (    
        <div>
            <h1>Projects</h1>
            <ul className="py-8 grid grid-cols-1 gap-4">
                {projectList && projectList.map(f => (
                    <li key={f.Name}>
                        <Link to={`/releases/${f.Name}`}>
                            <Card title={f.Name} />
                        </Link>
                    </li>
                ))}
            </ul>
        </div>
    )
}